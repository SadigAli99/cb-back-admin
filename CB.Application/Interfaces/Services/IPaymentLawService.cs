
using CB.Application.DTOs.PaymentLaw;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentLawService
    {
        Task<List<PaymentLawGetDTO>> GetAllAsync();
        Task<PaymentLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
