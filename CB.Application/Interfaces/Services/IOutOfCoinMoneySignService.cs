
using CB.Application.DTOs.OutOfCoinMoneySign;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCoinMoneySignService
    {
        Task<List<OutOfCoinMoneySignGetDTO>> GetAllAsync();
        Task<OutOfCoinMoneySignGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfCoinMoneySignCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfCoinMoneySignEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
