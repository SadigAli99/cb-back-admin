
using CB.Application.DTOs.RegistrationSecurity;

namespace CB.Application.Interfaces.Services
{
    public interface IRegistrationSecurityService
    {
        Task<List<RegistrationSecurityGetDTO>> GetAllAsync();
        Task<RegistrationSecurityGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RegistrationSecurityCreateDTO dto);
        Task<bool> UpdateAsync(int id, RegistrationSecurityEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
