
using CB.Application.DTOs.MacroEconomy;

namespace CB.Application.Interfaces.Services
{
    public interface IMacroEconomyService
    {
        Task<List<MacroEconomyGetDTO>> GetAllAsync();
        Task<MacroEconomyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MacroEconomyCreateDTO dto);
        Task<bool> UpdateAsync(int id, MacroEconomyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
