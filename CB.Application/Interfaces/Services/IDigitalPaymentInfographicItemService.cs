using CB.Application.DTOs.DigitalPaymentInfographicItem;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfographicItemService
    {
        Task<List<DigitalPaymentInfographicItemGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfographicItemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfographicItemCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfographicItemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
