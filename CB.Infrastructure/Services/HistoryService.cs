using AutoMapper;
using CB.Application.DTOs.History;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IGenericRepository<History> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public HistoryService(
            IMapper mapper,
            IGenericRepository<History> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(HistoryPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            History? hero = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (hero is null)
            {
                hero = _mapper.Map<History>(dto);

                hero.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new HistoryTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Description = description ?? string.Empty,
                    };
                }).ToList();

                result = await _repository.AddAsync(hero);
            }
            else
            {
                _mapper.Map(dto, hero);

                foreach (var v in dto.Titles)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    var existingTranslation = hero.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                        existingTranslation.Description = description ?? string.Empty;
                    }
                    else
                    {
                        hero.Translations.Add(new HistoryTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value,
                            Description = description ?? string.Empty,
                        });
                    }
                }

                result = await _repository.UpdateAsync(hero);
            }


            return result;
        }

        public async Task<HistoryGetDTO?> GetFirst()
        {
            History hero = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return hero == null ? null : _mapper.Map<HistoryGetDTO>(hero);
        }
    }
}
