using AutoMapper;
using CB.Application.DTOs.InvestmentFundCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class InvestmentFundCaptionService : IInvestmentFundCaptionService
    {
        private readonly IGenericRepository<InvestmentFundCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public InvestmentFundCaptionService(
            IMapper mapper,
            IGenericRepository<InvestmentFundCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(InvestmentFundCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            InvestmentFundCaption? investmentFundCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (investmentFundCaption is null)
            {
                investmentFundCaption = _mapper.Map<InvestmentFundCaption>(dto);

                investmentFundCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new InvestmentFundCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(investmentFundCaption);
            }
            else
            {
                _mapper.Map(dto, investmentFundCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = investmentFundCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        investmentFundCaption.Translations.Add(new InvestmentFundCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(investmentFundCaption);
            }


            return result;
        }

        public async Task<InvestmentFundCaptionGetDTO?> GetFirst()
        {
            InvestmentFundCaption? investmentFundCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return investmentFundCaption == null ? null : _mapper.Map<InvestmentFundCaptionGetDTO>(investmentFundCaption);
        }
    }
}
