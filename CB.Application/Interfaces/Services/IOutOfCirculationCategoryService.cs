
using CB.Application.DTOs.OutOfCirculationCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCirculationCategoryService
    {
        Task<List<OutOfCirculationCategoryGetDTO>> GetAllAsync();
        Task<OutOfCirculationCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfCirculationCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfCirculationCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
