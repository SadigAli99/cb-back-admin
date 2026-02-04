using AutoMapper;
using CB.Application.DTOs.NonBankCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class NonBankCaptionService : INonBankCaptionService
    {
        private readonly IGenericRepository<NonBankCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public NonBankCaptionService(
            IMapper mapper,
            IGenericRepository<NonBankCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(NonBankCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            NonBankCaption? nonBankCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (nonBankCaption is null)
            {
                nonBankCaption = _mapper.Map<NonBankCaption>(dto);

                nonBankCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new NonBankCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(nonBankCaption);
            }
            else
            {
                _mapper.Map(dto, nonBankCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = nonBankCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        nonBankCaption.Translations.Add(new NonBankCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(nonBankCaption);
            }


            return result;
        }

        public async Task<NonBankCaptionGetDTO?> GetFirst()
        {
            NonBankCaption? nonBankCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return nonBankCaption == null ? null : _mapper.Map<NonBankCaptionGetDTO>(nonBankCaption);
        }
    }
}
