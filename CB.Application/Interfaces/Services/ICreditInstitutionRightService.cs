
using CB.Application.DTOs.CreditInstitutionRight;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionRightService
    {
        Task<List<CreditInstitutionRightGetDTO>> GetAllAsync();
        Task<CreditInstitutionRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
