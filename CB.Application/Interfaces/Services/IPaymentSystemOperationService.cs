
using CB.Application.DTOs.PaymentSystemOperation;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemOperationService
    {
        Task<List<PaymentSystemOperationGetDTO>> GetAllAsync();
        Task<PaymentSystemOperationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemOperationCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemOperationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
