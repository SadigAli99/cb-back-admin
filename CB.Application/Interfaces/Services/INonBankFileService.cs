
using CB.Application.DTOs.NonBankFile;

namespace CB.Application.Interfaces.Services
{
    public interface INonBankFileService
    {
        Task<List<NonBankFileGetDTO>> GetAllAsync();
        Task<NonBankFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NonBankFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, NonBankFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
