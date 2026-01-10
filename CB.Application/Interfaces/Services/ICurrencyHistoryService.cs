using CB.Application.DTOs.CurrencyHistory;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryService
    {
        Task<List<CurrencyHistoryGetDTO>> GetAllAsync();
        Task<CurrencyHistoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
