
using CB.Application.DTOs.StockExchange;

namespace CB.Application.Interfaces.Services
{
    public interface IStockExchangeService
    {
        Task<List<StockExchangeGetDTO>> GetAllAsync();
        Task<StockExchangeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StockExchangeCreateDTO dto);
        Task<bool> UpdateAsync(int id, StockExchangeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
