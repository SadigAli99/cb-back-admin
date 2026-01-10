
using CB.Application.DTOs.CreditInstitutionPresidentAct;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionPresidentActService
    {
        Task<List<CreditInstitutionPresidentActGetDTO>> GetAllAsync();
        Task<CreditInstitutionPresidentActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditInstitutionPresidentActCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditInstitutionPresidentActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
