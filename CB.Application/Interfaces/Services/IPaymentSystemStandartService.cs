
using CB.Application.DTOs.PaymentSystemStandart;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemStandartService
    {
        Task<List<PaymentSystemStandartGetDTO>> GetAllAsync();
        Task<PaymentSystemStandartGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemStandartCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemStandartEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
