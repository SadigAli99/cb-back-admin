
using CB.Application.DTOs.ReceptionCitizenVideo;

namespace CB.Application.Interfaces.Services
{
    public interface IReceptionCitizenVideoService
    {
        Task<List<ReceptionCitizenVideoGetDTO>> GetAllAsync();
        Task<ReceptionCitizenVideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReceptionCitizenVideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReceptionCitizenVideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
