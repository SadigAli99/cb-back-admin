
using CB.Application.DTOs.Disclosure;

namespace CB.Application.Interfaces.Services
{
    public interface IDisclosureService
    {
        Task<List<DisclosureGetDTO>> GetAllAsync();
        Task<DisclosureGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DisclosureCreateDTO dto);
        Task<bool> UpdateAsync(int id, DisclosureEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
