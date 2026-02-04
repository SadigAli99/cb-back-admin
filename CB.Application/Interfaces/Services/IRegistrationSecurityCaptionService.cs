
using CB.Application.DTOs.RegistrationSecurityCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IRegistrationSecurityCaptionService
    {
        Task<bool> CreateOrUpdate(RegistrationSecurityCaptionPostDTO dTO);
        Task<RegistrationSecurityCaptionGetDTO?> GetFirst();
    }
}
