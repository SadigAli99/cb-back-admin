
using CB.Application.DTOs.BankFile;

namespace CB.Application.Interfaces.Services
{
    public interface IBankFileService
    {
        Task<List<BankFileGetDTO>> GetAllAsync();
        Task<BankFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
