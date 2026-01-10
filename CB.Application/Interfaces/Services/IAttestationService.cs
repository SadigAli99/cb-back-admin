
using CB.Application.DTOs.Attestation;

namespace CB.Application.Interfaces.Services
{
    public interface IAttestationService
    {
        Task<List<AttestationGetDTO>> GetAllAsync();
        Task<AttestationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AttestationCreateDTO dto);
        Task<bool> UpdateAsync(int id, AttestationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
