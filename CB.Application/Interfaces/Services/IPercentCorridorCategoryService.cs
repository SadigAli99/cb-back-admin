using CB.Application.DTOs.PercentCorridorCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IPercentCorridorCategoryService
    {
        Task<List<PercentCorridorCategoryGetDTO>> GetAllAsync();
        Task<PercentCorridorCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PercentCorridorCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, PercentCorridorCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
