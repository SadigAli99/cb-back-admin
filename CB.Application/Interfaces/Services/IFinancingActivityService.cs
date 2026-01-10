
using CB.Application.DTOs.FinancingActivity;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancingActivityService
    {
        Task<List<FinancingActivityGetDTO>> GetAllAsync();
        Task<FinancingActivityGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancingActivityCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancingActivityEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
