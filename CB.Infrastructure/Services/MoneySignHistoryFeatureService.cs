using AutoMapper;
using CB.Application.DTOs.MoneySignHistoryFeature;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MoneySignHistoryFeatureService : IMoneySignHistoryFeatureService
    {
        private readonly IGenericRepository<MoneySignHistoryFeature> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public MoneySignHistoryFeatureService(
            IMapper mapper,
            IGenericRepository<MoneySignHistoryFeature> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(MoneySignHistoryFeaturePostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            MoneySignHistoryFeature? entity = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (entity is null)
            {
                entity = _mapper.Map<MoneySignHistoryFeature>(dto);

                entity.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new MoneySignHistoryFeatureTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(entity);
            }
            else
            {
                _mapper.Map(dto, entity);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = entity.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        entity.Translations.Add(new MoneySignHistoryFeatureTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(entity);
            }


            return result;
        }

        public async Task<MoneySignHistoryFeatureGetDTO?> GetFirst()
        {
            MoneySignHistoryFeature? entity = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .Include(x => x.MoneySignHistory)
                .ThenInclude(x => x.Translations)
                .FirstOrDefaultAsync(h => h.Id == 1);

            if (entity is null) return null;

            var data = _mapper.Map<MoneySignHistoryFeatureGetDTO>(entity);
            data.MoneySignHistoryTitle = entity.MoneySignHistory.Translations.FirstOrDefault(x => x.LanguageId == 1)?.Title;

            return data;
        }
    }
}
