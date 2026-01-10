
using CB.Application.DTOs.InternshipCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInternshipCaptionService
    {
        Task<bool> CreateOrUpdate(InternshipCaptionPostDTO dTO);
        Task<InternshipCaptionGetDTO> GetFirst();
    }
}
