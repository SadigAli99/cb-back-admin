
using CB.Application.DTOs.InternationalEvent;

namespace CB.Application.Interfaces.Services
{
    public interface IInternationalEventService
    {
        Task<List<InternationalEventGetDTO>> GetAllAsync();
        Task<InternationalEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InternationalEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, InternationalEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
