
using CB.Application.DTOs.GovernmentPaymentPortal;

namespace CB.Application.Interfaces.Services
{
    public interface IGovernmentPaymentPortalService
    {
        Task<bool> CreateOrUpdate(GovernmentPaymentPortalPostDTO dTO);
        Task<GovernmentPaymentPortalGetDTO> GetFirst();
    }
}
