using CB.Application.DTOs.ControlMeasureCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IControlMeasureCategoryService
    {
        Task<List<ControlMeasureCategoryGetDTO>> GetAllAsync();
        Task<ControlMeasureCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ControlMeasureCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, ControlMeasureCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
