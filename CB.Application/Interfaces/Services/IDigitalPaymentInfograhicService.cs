using CB.Application.DTOs.DigitalPaymentInfograhic;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfograhicService
    {
        Task<List<DigitalPaymentInfograhicGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfograhicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfograhicCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfograhicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
