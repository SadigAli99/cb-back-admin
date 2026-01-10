using CB.Application.DTOs.CreditInstitutionCategory;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionCategoryService
    {
        Task<List<CreditInstitutionCategoryGetDTO>> GetAllAsync();
        Task<CreditInstitutionCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
