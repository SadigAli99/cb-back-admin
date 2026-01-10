
using CB.Application.DTOs.OperatorBank;

namespace CB.Application.Interfaces.Services
{
    public interface IOperatorBankService
    {
        Task<List<OperatorBankGetDTO>> GetAllAsync();
        Task<OperatorBankGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OperatorBankCreateDTO dto);
        Task<bool> UpdateAsync(int id, OperatorBankEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
