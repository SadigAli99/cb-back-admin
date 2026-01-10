
using CB.Application.DTOs.CBAR100Video;

namespace CB.Application.Interfaces.Services
{
    public interface ICBAR100VideoService
    {
        Task<List<CBAR100VideoGetDTO>> GetAllAsync();
        Task<CBAR100VideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CBAR100VideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, CBAR100VideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
