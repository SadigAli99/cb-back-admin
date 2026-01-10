using AutoMapper;
using CB.Application.DTOs.MissionCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MissionCaptionService : IMissionCaptionService
    {
        private readonly IGenericRepository<MissionCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public MissionCaptionService(
            IMapper mapper,
            IGenericRepository<MissionCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(MissionCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            MissionCaption? hero = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (hero is null)
            {
                hero = _mapper.Map<MissionCaption>(dto);

                hero.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Descriptions.TryGetValue(v.Key, out var description);

                    return new MissionCaptionTranslation
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
                        hero.Translations.Add(new MissionCaptionTranslation
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

        public async Task<MissionCaptionGetDTO?> GetFirst()
        {
            MissionCaption hero = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return hero == null ? null : _mapper.Map<MissionCaptionGetDTO>(hero);
        }
    }
}
