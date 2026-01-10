
using CB.Application.DTOs.PaymentInstitution;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentInstitutionService
    {
        Task<List<PaymentInstitutionGetDTO>> GetAllAsync();
        Task<PaymentInstitutionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentInstitutionCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentInstitutionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
