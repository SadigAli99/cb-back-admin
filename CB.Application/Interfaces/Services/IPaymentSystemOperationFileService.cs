
using CB.Application.DTOs.PaymentSystemOperationFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentSystemOperationFileService
    {
        Task<List<PaymentSystemOperationFileGetDTO>> GetAllAsync();
        Task<PaymentSystemOperationFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentSystemOperationFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentSystemOperationFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
