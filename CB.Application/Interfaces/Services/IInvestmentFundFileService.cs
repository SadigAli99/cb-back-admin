
using CB.Application.DTOs.InvestmentFundFile;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentFundFileService
    {
        Task<List<InvestmentFundFileGetDTO>> GetAllAsync();
        Task<InvestmentFundFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InvestmentFundFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, InvestmentFundFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
