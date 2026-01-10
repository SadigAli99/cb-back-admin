using CB.Application.DTOs.CBAR100Gallery;

namespace CB.Application.Interfaces.Services
{
    public interface ICBAR100GalleryService
    {
        Task<List<CBAR100GalleryGetDTO>> GetAllAsync();
        Task<CBAR100GalleryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CBAR100GalleryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CBAR100GalleryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
