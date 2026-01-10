
using CB.Application.DTOs.InvestmentFund;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentFundService
    {
        Task<List<InvestmentFundGetDTO>> GetAllAsync();
        Task<InvestmentFundGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InvestmentFundCreateDTO dto);
        Task<bool> UpdateAsync(int id, InvestmentFundEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
