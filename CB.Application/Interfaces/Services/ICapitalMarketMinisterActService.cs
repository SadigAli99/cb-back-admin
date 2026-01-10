
using CB.Application.DTOs.CapitalMarketMinisterAct;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketMinisterActService
    {
        Task<List<CapitalMarketMinisterActGetDTO>> GetAllAsync();
        Task<CapitalMarketMinisterActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketMinisterActCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketMinisterActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
