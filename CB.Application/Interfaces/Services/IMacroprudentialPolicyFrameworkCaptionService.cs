
using CB.Application.DTOs.MacroprudentialPolicyFrameworkCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IMacroprudentialPolicyFrameworkCaptionService
    {
        Task<bool> CreateOrUpdate(MacroprudentialPolicyFrameworkCaptionPostDTO dTO);
        Task<MacroprudentialPolicyFrameworkCaptionGetDTO> GetFirst();
    }
}
