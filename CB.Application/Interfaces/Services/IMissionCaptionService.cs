
using CB.Application.DTOs.MissionCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IMissionCaptionService
    {
        Task<bool> CreateOrUpdate(MissionCaptionPostDTO dTO);
        Task<MissionCaptionGetDTO?> GetFirst();
    }
}
