
using CB.Application.DTOs.CurrencyHistoryNext;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryNextService
    {
        Task<List<CurrencyHistoryNextGetDTO>> GetAllAsync();
        Task<CurrencyHistoryNextGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryNextCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryNextEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
