using AutoMapper;
using CB.Application.DTOs.InvestmentCompanyCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InvestmentCompanyCaptionService : IInvestmentCompanyCaptionService
    {
        private readonly IGenericRepository<InvestmentCompanyCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InvestmentCompanyCaptionService(
            IMapper mapper,
            IGenericRepository<InvestmentCompanyCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(InvestmentCompanyCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            InvestmentCompanyCaption? investmentCompanyCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (investmentCompanyCaption is null)
            {
                investmentCompanyCaption = _mapper.Map<InvestmentCompanyCaption>(dto);

                investmentCompanyCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new InvestmentCompanyCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(investmentCompanyCaption);
            }
            else
            {
                _mapper.Map(dto, investmentCompanyCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = investmentCompanyCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        investmentCompanyCaption.Translations.Add(new InvestmentCompanyCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(investmentCompanyCaption);
            }


            return result;
        }

        public async Task<InvestmentCompanyCaptionGetDTO?> GetFirst()
        {
            InvestmentCompanyCaption? investmentCompanyCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return investmentCompanyCaption == null ? null : _mapper.Map<InvestmentCompanyCaptionGetDTO>(investmentCompanyCaption);
        }
    }
}
