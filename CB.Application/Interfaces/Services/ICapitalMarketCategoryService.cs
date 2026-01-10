using CB.Application.DTOs.CapitalMarketCategory;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketCategoryService
    {
        Task<List<CapitalMarketCategoryGetDTO>> GetAllAsync();
        Task<CapitalMarketCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
