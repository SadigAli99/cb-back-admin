
using CB.Application.DTOs.PaymentService;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentServiceService
    {
        Task<List<PaymentServiceGetDTO>> GetAllAsync();
        Task<PaymentServiceGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentServiceCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentServiceEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
