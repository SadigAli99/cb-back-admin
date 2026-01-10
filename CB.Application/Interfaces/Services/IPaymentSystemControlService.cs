
using CB.Application.DTOs.PaymentSystemControl;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemControlService
    {
        Task<List<PaymentSystemControlGetDTO>> GetAllAsync();
        Task<PaymentSystemControlGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemControlCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemControlEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
