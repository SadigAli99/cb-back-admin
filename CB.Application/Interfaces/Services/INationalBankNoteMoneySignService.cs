
using CB.Application.DTOs.NationalBankNoteMoneySign;

namespace CB.Application.Interfaces.Services
{
    public interface INationalBankNoteMoneySignService
    {
        Task<List<NationalBankNoteMoneySignGetDTO>> GetAllAsync();
        Task<NationalBankNoteMoneySignGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NationalBankNoteMoneySignCreateDTO dto);
        Task<bool> UpdateAsync(int id, NationalBankNoteMoneySignEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
