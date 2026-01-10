
using CB.Application.DTOs.Gallery;

namespace CB.Application.Interfaces.Services
{
    public interface IGalleryService
    {
        Task<List<GalleryGetDTO>> GetAllAsync();
        Task<GalleryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(GalleryCreateDTO dto);
        Task<bool> UpdateAsync(int id, GalleryEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
