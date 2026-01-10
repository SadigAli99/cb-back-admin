using CB.Application.DTOs.MonetaryIndicatorCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryIndicatorCategoryService
    {
        Task<List<MonetaryIndicatorCategoryGetDTO>> GetAllAsync();
        Task<MonetaryIndicatorCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryIndicatorCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryIndicatorCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
