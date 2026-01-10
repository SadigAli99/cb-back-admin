
using CB.Application.DTOs.CapitalMarket;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketService
    {
        Task<List<CapitalMarketGetDTO>> GetAllAsync();
        Task<CapitalMarketGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
