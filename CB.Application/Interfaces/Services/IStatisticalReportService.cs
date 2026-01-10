
using CB.Application.DTOs.StatisticalReport;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticalReportService
    {
        Task<List<StatisticalReportGetDTO>> GetAllAsync();
        Task<StatisticalReportGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatisticalReportCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatisticalReportEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
