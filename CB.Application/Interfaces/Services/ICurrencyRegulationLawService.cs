
using CB.Application.DTOs.CurrencyRegulationLaw;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyRegulationLawService
    {
        Task<List<CurrencyRegulationLawGetDTO>> GetAllAsync();
        Task<CurrencyRegulationLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyRegulationLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyRegulationLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
