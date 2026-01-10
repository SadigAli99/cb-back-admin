using CB.Application.DTOs.CurrencyHistoryPrevItem;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryPrevItemService
    {
        Task<List<CurrencyHistoryPrevItemGetDTO>> GetAllAsync();
        Task<CurrencyHistoryPrevItemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryPrevItemCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
