
using CB.Application.DTOs.CapitalMarketPresidentAct;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketPresidentActService
    {
        Task<List<CapitalMarketPresidentActGetDTO>> GetAllAsync();
        Task<CapitalMarketPresidentActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketPresidentActCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketPresidentActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
