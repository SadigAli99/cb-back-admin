using AutoMapper;
using CB.Application.DTOs.OperatorBankCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class OperatorBankCaptionService : IOperatorBankCaptionService
    {
        private readonly IGenericRepository<OperatorBankCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public OperatorBankCaptionService(
            IMapper mapper,
            IGenericRepository<OperatorBankCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(OperatorBankCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            OperatorBankCaption? about = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (about is null)
            {
                about = _mapper.Map<OperatorBankCaption>(dto);

                about.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new OperatorBankCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(about);
            }
            else
            {
                _mapper.Map(dto, about);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = about.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        about.Translations.Add(new OperatorBankCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(about);
            }


            return result;
        }

        public async Task<OperatorBankCaptionGetDTO?> GetFirst()
        {
            OperatorBankCaption about = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return about == null ? null : _mapper.Map<OperatorBankCaptionGetDTO>(about);
        }
    }
}
