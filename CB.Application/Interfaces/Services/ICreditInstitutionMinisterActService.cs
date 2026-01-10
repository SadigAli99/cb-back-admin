
using CB.Application.DTOs.CreditInstitutionMinisterAct;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionMinisterActService
    {
        Task<List<CreditInstitutionMinisterActGetDTO>> GetAllAsync();
        Task<CreditInstitutionMinisterActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionMinisterActCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionMinisterActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
