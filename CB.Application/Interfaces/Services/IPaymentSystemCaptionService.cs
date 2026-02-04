
using CB.Application.DTOs.PaymentSystemCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemCaptionService
    {
        Task<bool> CreateOrUpdate(PaymentSystemCaptionPostDTO dTO);
        Task<PaymentSystemCaptionGetDTO?> GetFirst();
    }
}
