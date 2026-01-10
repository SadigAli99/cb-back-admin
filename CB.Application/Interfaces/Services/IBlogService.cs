
using CB.Application.DTOs.Blog;

namespace CB.Application.Interfaces.Services
{
    public interface IBlogService
    {
        Task<List<BlogGetDTO>> GetAllAsync();
        Task<BlogGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BlogCreateDTO dto);
        Task<bool> UpdateAsync(int id, BlogEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
