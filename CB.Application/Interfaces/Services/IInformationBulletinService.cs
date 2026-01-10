
using CB.Application.DTOs.InformationBulletin;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationBulletinService
    {
        Task<List<InformationBulletinGetDTO>> GetAllAsync();
        Task<InformationBulletinGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InformationBulletinCreateDTO dto);
        Task<bool> UpdateAsync(int id, InformationBulletinEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
