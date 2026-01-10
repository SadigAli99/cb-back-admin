using CB.Application.DTOs.CitizenApplicationCategory;

namespace CB.Application.Interfaces.Services
{
    public interface ICitizenApplicationCategoryService
    {
        Task<List<CitizenApplicationCategoryGetDTO>> GetAllAsync();
        Task<CitizenApplicationCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CitizenApplicationCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CitizenApplicationCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
