using AutoMapper;
using CB.Application.DTOs.PaymentInstitutionCaption;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class PaymentInstitutionCaptionService : IPaymentInstitutionCaptionService
    {
        private readonly IGenericRepository<PaymentInstitutionCaption> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public PaymentInstitutionCaptionService(
            IMapper mapper,
            IGenericRepository<PaymentInstitutionCaption> repository,
            IGenericRepository<Language> languageRepository
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
        }

        public async Task<bool> CreateOrUpdate(PaymentInstitutionCaptionPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            PaymentInstitutionCaption? paymentInstitutionCaption = await _repository.GetQuery()
                                        .Include(h => h.Translations)
                                        .ThenInclude(ht => ht.Language)
                                        .FirstOrDefaultAsync();

            bool result;

            if (paymentInstitutionCaption is null)
            {
                paymentInstitutionCaption = _mapper.Map<PaymentInstitutionCaption>(dto);

                paymentInstitutionCaption.Translations = dto.Descriptions.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    return new PaymentInstitutionCaptionTranslation
                    {
                        LanguageId = lang.Id,
                        Description = v.Value,
                    };
                }).ToList();

                result = await _repository.AddAsync(paymentInstitutionCaption);
            }
            else
            {
                _mapper.Map(dto, paymentInstitutionCaption);

                foreach (var v in dto.Descriptions)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    var existingTranslation = paymentInstitutionCaption.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Description = v.Value;
                    }
                    else
                    {
                        paymentInstitutionCaption.Translations.Add(new PaymentInstitutionCaptionTranslation
                        {
                            LanguageId = lang.Id,
                            Description = v.Value
                        });
                    }
                }

                result = await _repository.UpdateAsync(paymentInstitutionCaption);
            }


            return result;
        }

        public async Task<PaymentInstitutionCaptionGetDTO?> GetFirst()
        {
            PaymentInstitutionCaption? paymentInstitutionCaption = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return paymentInstitutionCaption == null ? null : _mapper.Map<PaymentInstitutionCaptionGetDTO>(paymentInstitutionCaption);
        }
    }
}
