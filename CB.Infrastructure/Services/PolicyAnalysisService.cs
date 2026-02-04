using AutoMapper;
using CB.Application.DTOs.PolicyAnalysis;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PolicyAnalysisService : IPolicyAnalysisService
    {
        private readonly IGenericRepository<PolicyAnalysis> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public PolicyAnalysisService(
            IMapper mapper,
            IGenericRepository<PolicyAnalysis> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(PolicyAnalysisPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            PolicyAnalysis? policyAnalysis = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (policyAnalysis is null)
            {
                policyAnalysis = _mapper.Map<PolicyAnalysis>(dto);

                policyAnalysis.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new PolicyAnalysisTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(policyAnalysis);
            }
            else
            {
                _mapper.Map(dto, policyAnalysis);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = policyAnalysis.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        policyAnalysis.Translations.Add(new PolicyAnalysisTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(policyAnalysis);
            }


            return result;
        }

        public async Task<PolicyAnalysisGetDTO?> GetFirst()
        {
            PolicyAnalysis? policyAnalysis = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return policyAnalysis == null ? null : _mapper.Map<PolicyAnalysisGetDTO>(policyAnalysis);
        }
    }
}
