
using CB.Application.DTOs.Mission;

namespace CB.Application.Interfaces.Services
{
    public interface IMissionService
    {
        Task<List<MissionGetDTO>> GetAllAsync();
        Task<MissionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MissionCreateDTO dto);
        Task<bool> UpdateAsync(int id, MissionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
