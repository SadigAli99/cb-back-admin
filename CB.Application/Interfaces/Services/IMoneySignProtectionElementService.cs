
using CB.Application.DTOs.MoneySignProtectionElement;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignProtectionElementService
    {
        Task<List<MoneySignProtectionElementGetDTO>> GetAllAsync(int? id);
        Task<MoneySignProtectionElementGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MoneySignProtectionElementCreateDTO dto);
        Task<bool> UpdateAsync(int id, MoneySignProtectionElementEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
