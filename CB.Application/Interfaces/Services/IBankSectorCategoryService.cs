using CB.Application.DTOs.BankSectorCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IBankSectorCategoryService
    {
        Task<List<BankSectorCategoryGetDTO>> GetAllAsync();
        Task<BankSectorCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankSectorCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankSectorCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
