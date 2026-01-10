
using CB.Application.DTOs.FinancialEvent;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialEventService
    {
        Task<List<FinancialEventGetDTO>> GetAllAsync();
        Task<FinancialEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
