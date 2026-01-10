
using CB.Application.DTOs.AnnualReport;

namespace CB.Application.Interfaces.Services
{
    public interface IAnnualReportService
    {
        Task<List<AnnualReportGetDTO>> GetAllAsync();
        Task<AnnualReportGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AnnualReportCreateDTO dto);
        Task<bool> UpdateAsync(int id, AnnualReportEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
