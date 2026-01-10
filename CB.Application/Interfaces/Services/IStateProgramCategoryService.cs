using CB.Application.DTOs.StateProgramCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IStateProgramCategoryService
    {
        Task<List<StateProgramCategoryGetDTO>> GetAllAsync();
        Task<StateProgramCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StateProgramCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, StateProgramCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
