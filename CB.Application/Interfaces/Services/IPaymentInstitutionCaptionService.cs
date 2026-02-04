
using CB.Application.DTOs.PaymentInstitutionCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentInstitutionCaptionService
    {
        Task<bool> CreateOrUpdate(PaymentInstitutionCaptionPostDTO dTO);
        Task<PaymentInstitutionCaptionGetDTO?> GetFirst();
    }
}
