using AutoMapper;
using CB.Application.DTOs.BankInvestmentCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class BankInvestmentCaptionService : IBankInvestmentCaptionService
    {
        private readonly IGenericRepository<BankInvestmentCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public BankInvestmentCaptionService(
            IMapper mapper,
            IGenericRepository<BankInvestmentCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(BankInvestmentCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            BankInvestmentCaption? bankInvestmentCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (bankInvestmentCaption is null)
            {
                bankInvestmentCaption = _mapper.Map<BankInvestmentCaption>(dto);

                bankInvestmentCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new BankInvestmentCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(bankInvestmentCaption);
            }
            else
            {
                _mapper.Map(dto, bankInvestmentCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = bankInvestmentCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        bankInvestmentCaption.Translations.Add(new BankInvestmentCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(bankInvestmentCaption);
            }


            return result;
        }

        public async Task<BankInvestmentCaptionGetDTO?> GetFirst()
        {
            BankInvestmentCaption? bankInvestmentCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return bankInvestmentCaption == null ? null : _mapper.Map<BankInvestmentCaptionGetDTO>(bankInvestmentCaption);
        }
    }
}
