using CB.Application.DTOs.CoinMoneySignHistory;

namespace CB.Application.Interfaces.Services
{
    public interface ICoinMoneySignHistoryService
    {
        Task<List<CoinMoneySignHistoryGetDTO>> GetAllAsync();
        Task<CoinMoneySignHistoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CoinMoneySignHistoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CoinMoneySignHistoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
