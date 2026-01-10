using CB.Application.DTOs.SecurityType;

namespace CB.Application.Interfaces.Services
{
    public interface ISecurityTypeService
    {
        Task<List<SecurityTypeGetDTO>> GetAllAsync();
        Task<SecurityTypeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SecurityTypeCreateDTO dto);
        Task<bool> UpdateAsync(int id, SecurityTypeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
