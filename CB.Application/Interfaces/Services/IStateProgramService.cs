
using CB.Application.DTOs.StateProgram;

namespace CB.Application.Interfaces.Services
{
    public interface IStateProgramService
    {
        Task<List<StateProgramGetDTO>> GetAllAsync();
        Task<StateProgramGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StateProgramCreateDTO dto);
        Task<bool> UpdateAsync(int id, StateProgramEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
