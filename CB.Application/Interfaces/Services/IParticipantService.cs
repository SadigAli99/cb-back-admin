
using CB.Application.DTOs.Participant;

namespace CB.Application.Interfaces.Services
{
    public interface IParticipantService
    {
        Task<List<ParticipantGetDTO>> GetAllAsync();
        Task<ParticipantGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ParticipantCreateDTO dto);
        Task<bool> UpdateAsync(int id, ParticipantEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
