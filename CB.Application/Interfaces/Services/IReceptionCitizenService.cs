
using CB.Application.DTOs.ReceptionCitizen;

namespace CB.Application.Interfaces.Services
{
    public interface IReceptionCitizenService
    {
        Task<List<ReceptionCitizenGetDTO>> GetAllAsync();
        Task<ReceptionCitizenGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReceptionCitizenCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReceptionCitizenEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
