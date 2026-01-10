
using CB.Application.DTOs.InternationalCooperationInitiative;

namespace CB.Application.Interfaces.Services
{
    public interface IInternationalCooperationInitiativeService
    {
        Task<List<InternationalCooperationInitiativeGetDTO>> GetAllAsync();
        Task<InternationalCooperationInitiativeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InternationalCooperationInitiativeCreateDTO dto);
        Task<bool> UpdateAsync(int id, InternationalCooperationInitiativeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
