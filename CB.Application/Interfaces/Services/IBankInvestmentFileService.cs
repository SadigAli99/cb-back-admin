
using CB.Application.DTOs.BankInvestmentFile;

namespace CB.Application.Interfaces.Services
{
    public interface IBankInvestmentFileService
    {
        Task<List<BankInvestmentFileGetDTO>> GetAllAsync();
        Task<BankInvestmentFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankInvestmentFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankInvestmentFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
