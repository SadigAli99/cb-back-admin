
using CB.Application.DTOs.CreditInstitution;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionService
    {
        Task<List<CreditInstitutionGetDTO>> GetAllAsync();
        Task<CreditInstitutionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
