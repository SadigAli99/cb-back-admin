
using CB.Application.DTOs.PaymentSystemStandartFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemStandartFileService
    {
        Task<List<PaymentSystemStandartFileGetDTO>> GetAllAsync();
        Task<PaymentSystemStandartFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemStandartFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemStandartFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
