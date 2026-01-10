using CB.Application.DTOs.CreditInstitutionSubCategory;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionSubCategoryService
    {
        Task<List<CreditInstitutionSubCategoryGetDTO>> GetAllAsync();
        Task<CreditInstitutionSubCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionSubCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionSubCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
