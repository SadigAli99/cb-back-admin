
using CB.Application.DTOs.PaymentSystemControlService;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemControlServiceService
    {
        Task<List<PaymentSystemControlServiceGetDTO>> GetAllAsync(int? id);
        Task<PaymentSystemControlServiceGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemControlServiceCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemControlServiceEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
