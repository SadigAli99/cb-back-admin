using CB.Application.DTOs.InfographicDisclosureCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IInfographicDisclosureCategoryService
    {
        Task<List<InfographicDisclosureCategoryGetDTO>> GetAllAsync();
        Task<InfographicDisclosureCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InfographicDisclosureCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, InfographicDisclosureCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
