
using CB.Application.DTOs.Nomination;

namespace CB.Application.Interfaces.Services
{
    public interface INominationService
    {
        Task<List<NominationGetDTO>> GetAllAsync();
        Task<NominationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NominationCreateDTO dto);
        Task<bool> UpdateAsync(int id, NominationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
