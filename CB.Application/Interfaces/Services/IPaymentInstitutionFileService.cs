
using CB.Application.DTOs.PaymentInstitutionFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentInstitutionFileService
    {
        Task<List<PaymentInstitutionFileGetDTO>> GetAllAsync();
        Task<PaymentInstitutionFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentInstitutionFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentInstitutionFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
