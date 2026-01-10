using CB.Application.DTOs.DigitalPaymentInfograhicCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfograhicCategoryService
    {
        Task<List<DigitalPaymentInfograhicCategoryGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfograhicCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfograhicCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfograhicCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
