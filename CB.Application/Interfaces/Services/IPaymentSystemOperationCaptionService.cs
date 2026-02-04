
using CB.Application.DTOs.PaymentSystemOperationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemOperationCaptionService
    {
        Task<bool> CreateOrUpdate(PaymentSystemOperationCaptionPostDTO dTO);
        Task<PaymentSystemOperationCaptionGetDTO?> GetFirst();
    }
}
