using CB.Application.DTOs.MoneySignHistory;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignHistoryService
    {
        Task<List<MoneySignHistoryGetDTO>> GetAllAsync();
        Task<MoneySignHistoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MoneySignHistoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, MoneySignHistoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
