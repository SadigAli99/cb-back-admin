using CB.Application.DTOs.NominationCategory;

namespace CB.Application.Interfaces.Services
{
    public interface INominationCategoryService
    {
        Task<List<NominationCategoryGetDTO>> GetAllAsync();
        Task<NominationCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NominationCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, NominationCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
