
using CB.Application.DTOs.DigitalPortalCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPortalCaptionService
    {
        Task<bool> CreateOrUpdate(DigitalPortalCaptionPostDTO dTO);
        Task<DigitalPortalCaptionGetDTO> GetFirst();
    }
}
