
using CB.Application.DTOs.Director;

namespace CB.Application.Interfaces.Services
{
    public interface IDirectorService
    {
        Task<List<DirectorGetDTO>> GetAllAsync();
        Task<DirectorGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DirectorCreateDTO dto);
        Task<bool> UpdateAsync(int id, DirectorEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
