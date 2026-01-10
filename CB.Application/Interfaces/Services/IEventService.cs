
using CB.Application.DTOs.Event;

namespace CB.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<EventGetDTO>> GetAllAsync();
        Task<EventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EventCreateDTO dto);
        Task<bool> UpdateAsync(int id, EventEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
