using AutoMapper;
using CB.Application.DTOs.PaymentSystemOperationCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaymentSystemOperationCaptionService : IPaymentSystemOperationCaptionService
    {
        private readonly IGenericRepository<PaymentSystemOperationCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public PaymentSystemOperationCaptionService(
            IMapper mapper,
            IGenericRepository<PaymentSystemOperationCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(PaymentSystemOperationCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            PaymentSystemOperationCaption? paymentSystemOperationCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (paymentSystemOperationCaption is null)
            {
                paymentSystemOperationCaption = _mapper.Map<PaymentSystemOperationCaption>(dto);

                paymentSystemOperationCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new PaymentSystemOperationCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(paymentSystemOperationCaption);
            }
            else
            {
                _mapper.Map(dto, paymentSystemOperationCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = paymentSystemOperationCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        paymentSystemOperationCaption.Translations.Add(new PaymentSystemOperationCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(paymentSystemOperationCaption);
            }


            return result;
        }

        public async Task<PaymentSystemOperationCaptionGetDTO?> GetFirst()
        {
            PaymentSystemOperationCaption? paymentSystemOperationCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return paymentSystemOperationCaption == null ? null : _mapper.Map<PaymentSystemOperationCaptionGetDTO>(paymentSystemOperationCaption);
        }
    }
}
