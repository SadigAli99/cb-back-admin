using AutoMapper;
using CB.Application.DTOs.StockExchangeCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class StockExchangeCaptionService : IStockExchangeCaptionService
    {
        private readonly IGenericRepository<StockExchangeCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public StockExchangeCaptionService(
            IMapper mapper,
            IGenericRepository<StockExchangeCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(StockExchangeCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            StockExchangeCaption? stockExchangeCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (stockExchangeCaption is null)
            {
                stockExchangeCaption = _mapper.Map<StockExchangeCaption>(dto);

                stockExchangeCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new StockExchangeCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(stockExchangeCaption);
            }
            else
            {
                _mapper.Map(dto, stockExchangeCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = stockExchangeCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        stockExchangeCaption.Translations.Add(new StockExchangeCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(stockExchangeCaption);
            }


            return result;
        }

        public async Task<StockExchangeCaptionGetDTO?> GetFirst()
        {
            StockExchangeCaption? stockExchangeCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return stockExchangeCaption == null ? null : _mapper.Map<StockExchangeCaptionGetDTO>(stockExchangeCaption);
        }
    }
}
