
using CB.Application.DTOs.InvestmentCompanyFile;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentCompanyFileService
    {
        Task<List<InvestmentCompanyFileGetDTO>> GetAllAsync();
        Task<InvestmentCompanyFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InvestmentCompanyFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, InvestmentCompanyFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
