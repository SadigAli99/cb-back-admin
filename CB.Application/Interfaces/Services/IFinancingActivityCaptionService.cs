
using CB.Application.DTOs.FinancingActivityCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancingActivityCaptionService
    {
        Task<bool> CreateOrUpdate(FinancingActivityCaptionPostDTO dTO);
        Task<FinancingActivityCaptionGetDTO> GetFirst();
    }
}
