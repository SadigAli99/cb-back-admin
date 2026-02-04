using CB.Application.DTOs.DigitalPaymentInfographicCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfographicCategoryService
    {
        Task<List<DigitalPaymentInfographicCategoryGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfographicCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfographicCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfographicCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
