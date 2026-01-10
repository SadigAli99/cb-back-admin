
using CB.Application.DTOs.PaymentSystemControlFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemControlFileService
    {
        Task<List<PaymentSystemControlFileGetDTO>> GetAllAsync();
        Task<PaymentSystemControlFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemControlFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemControlFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
