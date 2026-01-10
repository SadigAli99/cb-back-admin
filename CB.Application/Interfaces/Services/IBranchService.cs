using CB.Application.DTOs.Branch;

namespace CB.Application.Interfaces.Services
{
    public interface IBranchService
    {
        Task<List<BranchGetDTO>> GetAllAsync();
        Task<BranchGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BranchCreateDTO dto);
        Task<bool> UpdateAsync(int id, BranchEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
