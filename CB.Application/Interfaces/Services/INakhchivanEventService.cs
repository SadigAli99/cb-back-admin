
using CB.Application.DTOs.NakhchivanEvent;

namespace CB.Application.Interfaces.Services
{
    public interface INakhchivanEventService
    {
        Task<List<NakhchivanEventGetDTO>> GetAllAsync();
        Task<NakhchivanEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NakhchivanEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, NakhchivanEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
