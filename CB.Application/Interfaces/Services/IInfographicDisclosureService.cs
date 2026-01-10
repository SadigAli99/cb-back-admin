
using CB.Application.DTOs.InfographicDisclosure;

namespace CB.Application.Interfaces.Services
{
    public interface IInfographicDisclosureService
    {
        Task<List<InfographicDisclosureGetDTO>> GetAllAsync();
        Task<InfographicDisclosureGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InfographicDisclosureCreateDTO dto);
        Task<bool> UpdateAsync(int id, InfographicDisclosureEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
