
using CB.Application.DTOs.DigitalPortal;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPortalService
    {
        Task<List<DigitalPortalGetDTO>> GetAllAsync();
        Task<DigitalPortalGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPortalCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPortalEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
