
using CB.Application.DTOs.LicensingProcess;

namespace CB.Application.Interfaces.Services
{
    public interface ILicensingProcessService
    {
        Task<List<LicensingProcessGetDTO>> GetAllAsync();
        Task<LicensingProcessGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LicensingProcessCreateDTO dto);
        Task<bool> UpdateAsync(int id, LicensingProcessEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
