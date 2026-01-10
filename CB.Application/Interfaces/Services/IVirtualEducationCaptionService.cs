
using CB.Application.DTOs.VirtualEducationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IVirtualEducationCaptionService
    {
        Task<bool> CreateOrUpdate(VirtualEducationCaptionPostDTO dTO);
        Task<VirtualEducationCaptionGetDTO> GetFirst();
    }
}
