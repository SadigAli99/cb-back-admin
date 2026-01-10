
using CB.Application.DTOs.OtherRight;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherRightService
    {
        Task<List<OtherRightGetDTO>> GetAllAsync();
        Task<OtherRightGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OtherRightCreateDTO dto);
        Task<bool> UpdateAsync(int id, OtherRightEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
