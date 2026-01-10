
using CB.Application.DTOs.Video;

namespace CB.Application.Interfaces.Services
{
    public interface IVideoService
    {
        Task<List<VideoGetDTO>> GetAllAsync();
        Task<VideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, VideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
