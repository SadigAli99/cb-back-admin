
using CB.Application.DTOs.NonBank;

namespace CB.Application.Interfaces.Services
{
    public interface INonBankService
    {
        Task<List<NonBankGetDTO>> GetAllAsync();
        Task<NonBankGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NonBankCreateDTO dto);
        Task<bool> UpdateAsync(int id, NonBankEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
