
using CB.Application.DTOs.InsurerRight;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerRightService
    {
        Task<List<InsurerRightGetDTO>> GetAllAsync();
        Task<InsurerRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
