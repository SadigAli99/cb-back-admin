
using CB.Application.DTOs.NominationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface INominationCaptionService
    {
        Task<bool> CreateOrUpdate(NominationCaptionPostDTO dTO);
        Task<NominationCaptionGetDTO> GetFirst();
    }
}
