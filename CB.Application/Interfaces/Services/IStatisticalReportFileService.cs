
using CB.Application.DTOs.StatisticalReportFile;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticalReportFileService
    {
        Task<List<StatisticalReportFileGetDTO>> GetAllAsync();
        Task<StatisticalReportFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatisticalReportFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatisticalReportFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
