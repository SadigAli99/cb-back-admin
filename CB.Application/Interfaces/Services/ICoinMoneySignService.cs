
using CB.Application.DTOs.CoinMoneySign;

namespace CB.Application.Interfaces.Services
{
    public interface ICoinMoneySignService
    {
        Task<List<CoinMoneySignGetDTO>> GetAllAsync();
        Task<CoinMoneySignGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CoinMoneySignCreateDTO dto);
        Task<bool> UpdateAsync(int id, CoinMoneySignEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
