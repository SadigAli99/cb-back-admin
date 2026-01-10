
using CB.Application.DTOs.NakhchivanBulletin;

namespace CB.Application.Interfaces.Services
{
    public interface INakhchivanBulletinService
    {
        Task<List<NakhchivanBulletinGetDTO>> GetAllAsync();
        Task<NakhchivanBulletinGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NakhchivanBulletinCreateDTO dto);
        Task<bool> UpdateAsync(int id, NakhchivanBulletinEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
