
using CB.Application.DTOs.StatisticalBulletin;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticalBulletinService
    {
        Task<List<StatisticalBulletinGetDTO>> GetAllAsync();
        Task<StatisticalBulletinGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatisticalBulletinCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatisticalBulletinEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
