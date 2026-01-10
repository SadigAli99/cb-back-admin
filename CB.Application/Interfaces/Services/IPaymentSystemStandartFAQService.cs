
using CB.Application.DTOs.PaymentSystemStandartFAQ;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemStandartFAQService
    {
        Task<List<PaymentSystemStandartFAQGetDTO>> GetAllAsync(int? id);
        Task<PaymentSystemStandartFAQGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemStandartFAQCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemStandartFAQEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
