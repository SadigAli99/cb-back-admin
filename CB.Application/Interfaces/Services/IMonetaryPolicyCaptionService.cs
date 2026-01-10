
using CB.Application.DTOs.MonetaryPolicyCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyCaptionService
    {
        Task<bool> CreateOrUpdate(MonetaryPolicyCaptionPostDTO dTO);
        Task<MonetaryPolicyCaptionGetDTO> GetFirst();
    }
}
