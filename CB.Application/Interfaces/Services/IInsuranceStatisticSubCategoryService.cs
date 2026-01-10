using CB.Application.DTOs.InsuranceStatisticSubCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IInsuranceStatisticSubCategoryService
    {
        Task<List<InsuranceStatisticSubCategoryGetDTO>> GetAllAsync();
        Task<InsuranceStatisticSubCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsuranceStatisticSubCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsuranceStatisticSubCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
