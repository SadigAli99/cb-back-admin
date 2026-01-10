
using CB.Application.DTOs.CurrencyHistoryPrev;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryPrevService
    {
        Task<List<CurrencyHistoryPrevGetDTO>> GetAllAsync();
        Task<CurrencyHistoryPrevGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryPrevCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryPrevEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
