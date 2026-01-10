
using CB.Application.DTOs.PaymentRight;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentRightService
    {
        Task<List<PaymentRightGetDTO>> GetAllAsync();
        Task<PaymentRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
