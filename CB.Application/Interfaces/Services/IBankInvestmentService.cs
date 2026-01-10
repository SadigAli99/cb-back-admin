
using CB.Application.DTOs.BankInvestment;

namespace CB.Application.Interfaces.Services
{
    public interface IBankInvestmentService
    {
        Task<List<BankInvestmentGetDTO>> GetAllAsync();
        Task<BankInvestmentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankInvestmentCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankInvestmentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
