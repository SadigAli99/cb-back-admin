
using CB.Application.DTOs.InsurerCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerCaptionService
    {
        Task<bool> CreateOrUpdate(InsurerCaptionPostDTO dTO);
        Task<InsurerCaptionGetDTO> GetFirst();
    }
}
