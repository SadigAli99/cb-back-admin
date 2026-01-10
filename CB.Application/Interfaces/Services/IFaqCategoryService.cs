using CB.Application.DTOs.FaqCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IFaqCategoryService
    {
        Task<List<FaqCategoryGetDTO>> GetAllAsync();
        Task<FaqCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FaqCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, FaqCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
