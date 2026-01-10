
using CB.Application.DTOs.EventImage;

namespace CB.Application.Interfaces.Services
{
    public interface IEventImageService
    {
        Task<List<EventImageGetDTO>> GetAllAsync(int? id);
        Task<EventImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EventImageCreateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
