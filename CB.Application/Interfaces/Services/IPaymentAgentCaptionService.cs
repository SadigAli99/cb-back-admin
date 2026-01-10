
using CB.Application.DTOs.PaymentAgentCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentAgentCaptionService
    {
        Task<bool> CreateOrUpdate(PaymentAgentCaptionPostDTO dTO);
        Task<PaymentAgentCaptionGetDTO> GetFirst();
    }
}
