
using CB.Application.DTOs.FinancialStabilityReport;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialStabilityReportService
    {
        Task<List<FinancialStabilityReportGetDTO>> GetAllAsync();
        Task<FinancialStabilityReportGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialStabilityReportCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialStabilityReportEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
