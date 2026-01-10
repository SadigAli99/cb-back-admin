
using CB.Application.DTOs.NakhchivanBlog;

namespace CB.Application.Interfaces.Services
{
    public interface INakhchivanBlogService
    {
        Task<List<NakhchivanBlogGetDTO>> GetAllAsync();
        Task<NakhchivanBlogGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NakhchivanBlogCreateDTO dto);
        Task<bool> UpdateAsync(int id, NakhchivanBlogEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
