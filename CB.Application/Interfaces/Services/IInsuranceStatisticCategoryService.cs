using CB.Application.DTOs.InsuranceStatisticCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IInsuranceStatisticCategoryService
    {
        Task<List<InsuranceStatisticCategoryGetDTO>> GetAllAsync();
        Task<InsuranceStatisticCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsuranceStatisticCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsuranceStatisticCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
