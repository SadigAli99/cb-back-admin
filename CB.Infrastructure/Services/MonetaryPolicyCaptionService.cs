using AutoMapper;
using CB.Application.DTOs.MonetaryPolicyCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MonetaryPolicyCaptionService : IMonetaryPolicyCaptionService
    {
        private readonly IGenericRepository<MonetaryPolicyCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public MonetaryPolicyCaptionService(
            IMapper mapper,
            IGenericRepository<MonetaryPolicyCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(MonetaryPolicyCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            MonetaryPolicyCaption? monetaryPolicyCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (monetaryPolicyCaption is null)
            {
                monetaryPolicyCaption = _mapper.Map<MonetaryPolicyCaption>(dto);

                monetaryPolicyCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new MonetaryPolicyCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(monetaryPolicyCaption);
            }
            else
            {
                _mapper.Map(dto, monetaryPolicyCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = monetaryPolicyCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        monetaryPolicyCaption.Translations.Add(new MonetaryPolicyCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(monetaryPolicyCaption);
            }


            return result;
        }

        public async Task<MonetaryPolicyCaptionGetDTO?> GetFirst()
        {
            MonetaryPolicyCaption? monetaryPolicyCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return monetaryPolicyCaption == null ? null : _mapper.Map<MonetaryPolicyCaptionGetDTO>(monetaryPolicyCaption);
        }
    }
}
