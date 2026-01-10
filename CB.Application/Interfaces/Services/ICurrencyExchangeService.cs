
using CB.Application.DTOs.CurrencyExchange;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyExchangeService
    {
        Task<List<CurrencyExchangeGetDTO>> GetAllAsync();
        Task<CurrencyExchangeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyExchangeCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyExchangeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
