
using CB.Application.DTOs.FinancialStabilityReportCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialStabilityReportCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialStabilityReportCaptionPostDTO dTO);
        Task<FinancialStabilityReportCaptionGetDTO?> GetFirst();
    }
}
