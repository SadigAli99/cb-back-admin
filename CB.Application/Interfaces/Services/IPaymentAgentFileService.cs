
using CB.Application.DTOs.PaymentAgentFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentAgentFileService
    {
        Task<List<PaymentAgentFileGetDTO>> GetAllAsync();
        Task<PaymentAgentFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentAgentFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentAgentFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
