
using CB.Application.DTOs.DigitalPayment;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentService
    {
        Task<List<DigitalPaymentGetDTO>> GetAllAsync();
        Task<DigitalPaymentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
