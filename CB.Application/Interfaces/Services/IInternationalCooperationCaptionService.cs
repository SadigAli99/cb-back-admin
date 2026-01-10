
using CB.Application.DTOs.InternationalCooperationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInternationalCooperationCaptionService
    {
        Task<bool> CreateOrUpdate(InternationalCooperationCaptionPostDTO dTO);
        Task<InternationalCooperationCaptionGetDTO> GetFirst();
    }
}
