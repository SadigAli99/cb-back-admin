
using CB.Application.DTOs.ClearingSettlementSystemCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IClearingSettlementSystemCaptionService
    {
        Task<bool> CreateOrUpdate(ClearingSettlementSystemCaptionPostDTO dTO);
        Task<ClearingSettlementSystemCaptionGetDTO> GetFirst();
    }
}
