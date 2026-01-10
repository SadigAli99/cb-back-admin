
using CB.Application.DTOs.CreditInstitutionLaw;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionLawService
    {
        Task<List<CreditInstitutionLawGetDTO>> GetAllAsync();
        Task<CreditInstitutionLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
