
using CB.Application.DTOs.MoneySignProtection;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignProtectionService
    {
        Task<List<MoneySignProtectionGetDTO>> GetAllAsync();
        Task<MoneySignProtectionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MoneySignProtectionCreateDTO dto);
        Task<bool> UpdateAsync(int id, MoneySignProtectionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
