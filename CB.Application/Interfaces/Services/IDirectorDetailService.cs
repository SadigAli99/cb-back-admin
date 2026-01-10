
using CB.Application.DTOs.DirectorDetail;

namespace CB.Application.Interfaces.Services
{
    public interface IDirectorDetailService
    {
        Task<List<DirectorDetailGetDTO>> GetAllAsync(int? id);
        Task<DirectorDetailGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DirectorDetailCreateDTO dto);
        Task<bool> UpdateAsync(int id, DirectorDetailEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
