
using CB.Application.DTOs.CurrencyHistoryNextItem;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryNextItemService
    {
        Task<List<CurrencyHistoryNextItemGetDTO>> GetAllAsync();
        Task<CurrencyHistoryNextItemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryNextItemCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryNextItemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
