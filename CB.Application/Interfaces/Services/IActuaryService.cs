
using CB.Application.DTOs.Actuary;

namespace CB.Application.Interfaces.Services
{
    public interface IActuaryService
    {
        Task<List<ActuaryGetDTO>> GetAllAsync();
        Task<ActuaryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ActuaryCreateDTO dto);
        Task<bool> UpdateAsync(int id, ActuaryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
