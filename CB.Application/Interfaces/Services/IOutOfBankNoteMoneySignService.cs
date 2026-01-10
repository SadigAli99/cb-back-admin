
using CB.Application.DTOs.OutOfBankNoteMoneySign;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfBankNoteMoneySignService
    {
        Task<List<OutOfBankNoteMoneySignGetDTO>> GetAllAsync();
        Task<OutOfBankNoteMoneySignGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfBankNoteMoneySignCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
