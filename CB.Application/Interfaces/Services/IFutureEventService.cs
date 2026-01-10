
using CB.Application.DTOs.FutureEvent;

namespace CB.Application.Interfaces.Services
{
    public interface IFutureEventService
    {
        Task<List<FutureEventGetDTO>> GetAllAsync();
        Task<FutureEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FutureEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, FutureEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
