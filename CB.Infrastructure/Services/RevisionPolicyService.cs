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
            RevisionPolicy? revisionPolicy = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (revisionPolicy is null)
            {
                revisionPolicy = _mapper.Map<RevisionPolicy>(dto);

                revisionPolicy.Translations = dto.Descriptions.Select(v =>
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

                result = await _repository.AddAsync(revisionPolicy);
            }
            else
            {
                _mapper.Map(dto, revisionPolicy);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = revisionPolicy.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        revisionPolicy.Translations.Add(new RevisionPolicyTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(revisionPolicy);
            }


            return result;
        }

        public async Task<RevisionPolicyGetDTO?> GetFirst()
        {
            RevisionPolicy? revisionPolicy = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return revisionPolicy == null ? null : _mapper.Map<RevisionPolicyGetDTO>(revisionPolicy);
        }
    }
}
