
using CB.Application.DTOs.EconometricModelFile;

namespace CB.Application.Interfaces.Services
{
    public interface IEconometricModelFileService
    {
        Task<List<EconometricModelFileGetDTO>> GetAllAsync();
        Task<EconometricModelFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EconometricModelFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, EconometricModelFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
