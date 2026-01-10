
using CB.Application.DTOs.ReceptionCitizenLink;

namespace CB.Application.Interfaces.Services
{
    public interface IReceptionCitizenLinkService
    {
        Task<List<ReceptionCitizenLinkGetDTO>> GetAllAsync();
        Task<ReceptionCitizenLinkGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReceptionCitizenLinkCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReceptionCitizenLinkEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
