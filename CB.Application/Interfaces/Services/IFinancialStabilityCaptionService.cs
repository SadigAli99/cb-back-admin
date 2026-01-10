
using CB.Application.DTOs.FinancialStabilityCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialStabilityCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialStabilityCaptionPostDTO dTO);
        Task<FinancialStabilityCaptionGetDTO> GetFirst();
    }
}
