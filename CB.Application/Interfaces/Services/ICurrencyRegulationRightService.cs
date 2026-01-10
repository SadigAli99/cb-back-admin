
using CB.Application.DTOs.CurrencyRegulationRight;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyRegulationRightService
    {
        Task<List<CurrencyRegulationRightGetDTO>> GetAllAsync();
        Task<CurrencyRegulationRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyRegulationRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyRegulationRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
