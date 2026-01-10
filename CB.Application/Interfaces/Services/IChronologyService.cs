
using CB.Application.DTOs.Chronology;

namespace CB.Application.Interfaces.Services
{
    public interface IChronologyService
    {
        Task<List<ChronologyGetDTO>> GetAllAsync();
        Task<ChronologyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ChronologyCreateDTO dto);
        Task<bool> UpdateAsync(int id, ChronologyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
