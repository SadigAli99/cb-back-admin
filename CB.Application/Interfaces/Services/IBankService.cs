
using CB.Application.DTOs.Bank;

namespace CB.Application.Interfaces.Services
{
    public interface IBankService
    {
        Task<List<BankGetDTO>> GetAllAsync();
        Task<BankGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
