using AutoMapper;
using CB.Application.DTOs.RevisionPolicy;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class RevisionPolicyService : IRevisionPolicyService
    {
        private readonly IGenericRepository<RevisionPolicy> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public RevisionPolicyService(
            IMapper mapper,
            IGenericRepository<RevisionPolicy> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(RevisionPolicyPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            RevisionPolicy? hero = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (hero is null)
            {
                hero = _mapper.Map<RevisionPolicy>(dto);

                hero.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new RevisionPolicyTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(hero);
            }
            else
            {
                _mapper.Map(dto, hero);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = hero.Translations?.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        hero.Translations.Add(new RevisionPolicyTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(hero);
            }


            return result;
        }

        public async Task<RevisionPolicyGetDTO?> GetFirst()
        {
            RevisionPolicy hero = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return hero == null ? null : _mapper.Map<RevisionPolicyGetDTO>(hero);
        }
    }
}
