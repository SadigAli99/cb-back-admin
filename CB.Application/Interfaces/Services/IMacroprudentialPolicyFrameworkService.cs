
using CB.Application.DTOs.MacroprudentialPolicyFramework;

namespace CB.Application.Interfaces.Services
{
    public interface IMacroprudentialPolicyFrameworkService
    {
        Task<List<MacroprudentialPolicyFrameworkGetDTO>> GetAllAsync();
        Task<MacroprudentialPolicyFrameworkGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MacroprudentialPolicyFrameworkCreateDTO dto);
        Task<bool> UpdateAsync(int id, MacroprudentialPolicyFrameworkEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
