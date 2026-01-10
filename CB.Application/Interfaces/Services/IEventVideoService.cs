using CB.Application.DTOs.EventVideo;

namespace CB.Application.Interfaces.Services
{
    public interface IEventVideoService
    {
        Task<List<EventVideoGetDTO>> GetAllAsync(int? id);
        Task<EventVideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EventVideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, EventVideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
