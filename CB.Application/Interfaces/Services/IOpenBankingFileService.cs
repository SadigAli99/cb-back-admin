
using CB.Application.DTOs.OpenBankingFile;

namespace CB.Application.Interfaces.Services
{
    public interface IOpenBankingFileService
    {
        Task<List<OpenBankingFileGetDTO>> GetAllAsync();
        Task<OpenBankingFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OpenBankingFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, OpenBankingFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
