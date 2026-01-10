
using CB.Application.DTOs.ReceptionCitizenFile;

namespace CB.Application.Interfaces.Services
{
    public interface IReceptionCitizenFileService
    {
        Task<List<ReceptionCitizenFileGetDTO>> GetAllAsync();
        Task<ReceptionCitizenFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReceptionCitizenFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReceptionCitizenFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
