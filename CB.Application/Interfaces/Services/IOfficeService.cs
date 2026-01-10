
using CB.Application.DTOs.Office;

namespace CB.Application.Interfaces.Services
{
    public interface IOfficeService
    {
        Task<List<OfficeGetDTO>> GetAllAsync();
        Task<OfficeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OfficeCreateDTO dto);
        Task<bool> UpdateAsync(int id, OfficeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
