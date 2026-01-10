using CB.Application.DTOs.OutOfCoinMoneySignHistory;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCoinMoneySignHistoryService
    {
        Task<List<OutOfCoinMoneySignHistoryGetDTO>> GetAllAsync();
        Task<OutOfCoinMoneySignHistoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfCoinMoneySignHistoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfCoinMoneySignHistoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
