
using CB.Application.DTOs.CybersecurityStrategy;

namespace CB.Application.Interfaces.Services
{
    public interface ICybersecurityStrategyService
    {
        Task<List<CybersecurityStrategyGetDTO>> GetAllAsync();
        Task<CybersecurityStrategyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CybersecurityStrategyCreateDTO dto);
        Task<bool> UpdateAsync(int id, CybersecurityStrategyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
