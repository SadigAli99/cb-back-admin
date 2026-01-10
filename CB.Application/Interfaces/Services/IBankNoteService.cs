using CB.Application.DTOs.BankNote;

namespace CB.Application.Interfaces.Services
{
    public interface IBankNoteService
    {
        Task<List<BankNoteGetDTO>> GetAllAsync();
        Task<BankNoteGetDTO?> GetByIdAsync(int id);
        Task<BankNoteEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(BankNoteCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankNoteEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
