
using CB.Application.DTOs.LossAdjusterCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ILossAdjusterCaptionService
    {
        Task<bool> CreateOrUpdate(LossAdjusterCaptionPostDTO dTO);
        Task<LossAdjusterCaptionGetDTO?> GetFirst();
    }
}
