
using CB.Application.DTOs.OtherRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherRightCaptionService
    {
        Task<bool> CreateOrUpdate(OtherRightCaptionPostDTO dTO);
        Task<OtherRightCaptionGetDTO?> GetFirst();
    }
}
