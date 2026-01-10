using CB.Application.DTOs.OutOfBankNoteMoneySignHistory;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfBankNoteMoneySignHistoryService
    {
        Task<List<OutOfBankNoteMoneySignHistoryGetDTO>> GetAllAsync();
        Task<OutOfBankNoteMoneySignHistoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfBankNoteMoneySignHistoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignHistoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
