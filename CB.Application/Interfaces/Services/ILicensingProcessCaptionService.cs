
using CB.Application.DTOs.LicensingProcessCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ILicensingProcessCaptionService
    {
        Task<bool> CreateOrUpdate(LicensingProcessCaptionPostDTO dTO);
        Task<LicensingProcessCaptionGetDTO?> GetFirst();
    }
}
