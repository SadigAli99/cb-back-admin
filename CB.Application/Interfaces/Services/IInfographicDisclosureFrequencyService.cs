using CB.Application.DTOs.InfographicDisclosureFrequency;

namespace CB.Application.Interfaces.Services
{
    public interface IInfographicDisclosureFrequencyService
    {
        Task<List<InfographicDisclosureFrequencyGetDTO>> GetAllAsync();
        Task<InfographicDisclosureFrequencyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InfographicDisclosureFrequencyCreateDTO dto);
        Task<bool> UpdateAsync(int id, InfographicDisclosureFrequencyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
