
using CB.Application.DTOs.Poster;

namespace CB.Application.Interfaces.Services
{
    public interface IPosterService
    {
        Task<List<PosterGetDTO>> GetAllAsync();
        Task<PosterGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PosterCreateDTO dto);
        Task<bool> UpdateAsync(int id, PosterEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
