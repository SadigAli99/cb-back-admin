
using CB.Application.DTOs.CapitalMarketRight;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketRightService
    {
        Task<List<CapitalMarketRightGetDTO>> GetAllAsync();
        Task<CapitalMarketRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
