using CB.Application.DTOs.StatisticalReportCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticalReportCategoryService
    {
        Task<List<StatisticalReportCategoryGetDTO>> GetAllAsync();
        Task<StatisticalReportCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatisticalReportCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatisticalReportCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
