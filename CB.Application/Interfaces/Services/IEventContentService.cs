
using CB.Application.DTOs.EventContent;

namespace CB.Application.Interfaces.Services
{
    public interface IEventContentService
    {
        Task<List<EventContentGetDTO>> GetAllAsync(int? id);
        Task<EventContentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EventContentCreateDTO dto);
        Task<bool> UpdateAsync(int id, EventContentEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
