using CB.Application.DTOs.DigitalPaymentInfographic;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfographicService
    {
        Task<List<DigitalPaymentInfographicGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfographicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfographicCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfographicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
