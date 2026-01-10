using CB.Application.DTOs.BankNoteCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IBankNoteCategoryService
    {
        Task<List<BankNoteCategoryGetDTO>> GetAllAsync();
        Task<BankNoteCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankNoteCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankNoteCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
