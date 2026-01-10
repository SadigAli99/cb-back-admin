
using CB.Application.DTOs.InvestmentCompany;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentCompanyService
    {
        Task<List<InvestmentCompanyGetDTO>> GetAllAsync();
        Task<InvestmentCompanyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InvestmentCompanyCreateDTO dto);
        Task<bool> UpdateAsync(int id, InvestmentCompanyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
