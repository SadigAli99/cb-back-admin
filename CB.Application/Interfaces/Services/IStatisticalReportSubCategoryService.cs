using CB.Application.DTOs.StatisticalReportSubCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticalReportSubCategoryService
    {
        Task<List<StatisticalReportSubCategoryGetDTO>> GetAllAsync();
        Task<StatisticalReportSubCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatisticalReportSubCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatisticalReportSubCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
