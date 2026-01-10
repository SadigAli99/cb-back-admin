
using CB.Application.DTOs.CapitalMarketLaw;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketLawService
    {
        Task<List<CapitalMarketLawGetDTO>> GetAllAsync();
        Task<CapitalMarketLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
