using AutoMapper;
using CB.Application.DTOs.Hero;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class HeroService : IHeroService
    {
        private readonly IGenericRepository<Hero> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public HeroService(
            IMapper mapper,
            IGenericRepository<Hero> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(HeroPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            Hero? hero = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (hero is null)
            {
                hero = _mapper.Map<Hero>(dto);

                hero.Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new HeroTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value
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

                    var existingTranslation = hero.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = v.Value;
                    }
                    else
                    {
                        hero.Translations.Add(new HeroTranslation
                        {
                            LanguageId = lang.Id,
                            Title = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(hero);
            }


            return result;
        }

        public async Task<HeroGetDTO?> GetFirst()
        {
            Hero? hero = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return hero == null ? null : _mapper.Map<HeroGetDTO>(hero);
        }
    }
}
