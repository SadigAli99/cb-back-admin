
using CB.Application.DTOs.RealTimeSettlementSystemCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IRealTimeSettlementSystemCaptionService
    {
        Task<bool> CreateOrUpdate(RealTimeSettlementSystemCaptionPostDTO dTO);
        Task<RealTimeSettlementSystemCaptionGetDTO?> GetFirst();
    }
}
