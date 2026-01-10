
using CB.Application.DTOs.StockExchangeFile;

namespace CB.Application.Interfaces.Services
{
    public interface IStockExchangeFileService
    {
        Task<List<StockExchangeFileGetDTO>> GetAllAsync();
        Task<StockExchangeFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StockExchangeFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, StockExchangeFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
