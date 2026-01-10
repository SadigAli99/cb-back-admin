
using CB.Application.DTOs.Software;

namespace CB.Application.Interfaces.Services
{
    public interface ISoftwareService
    {
        Task<List<SoftwareGetDTO>> GetAllAsync();
        Task<SoftwareGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SoftwareCreateDTO dto);
        Task<bool> UpdateAsync(int id, SoftwareEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
