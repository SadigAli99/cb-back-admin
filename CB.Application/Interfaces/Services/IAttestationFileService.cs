
using CB.Application.DTOs.AttestationFile;

namespace CB.Application.Interfaces.Services
{
    public interface IAttestationFileService
    {
        Task<List<AttestationFileGetDTO>> GetAllAsync();
        Task<AttestationFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AttestationFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, AttestationFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
