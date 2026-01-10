
using CB.Application.DTOs.Insurer;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerService
    {
        Task<List<InsurerGetDTO>> GetAllAsync();
        Task<InsurerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
