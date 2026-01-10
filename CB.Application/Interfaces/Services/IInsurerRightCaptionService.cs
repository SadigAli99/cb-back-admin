
using CB.Application.DTOs.InsurerRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerRightCaptionService
    {
        Task<bool> CreateOrUpdate(InsurerRightCaptionPostDTO dTO);
        Task<InsurerRightCaptionGetDTO> GetFirst();
    }
}
