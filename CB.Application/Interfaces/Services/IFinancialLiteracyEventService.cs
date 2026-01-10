
using CB.Application.DTOs.FinancialLiteracyEvent;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialLiteracyEventService
    {
        Task<List<FinancialLiteracyEventGetDTO>> GetAllAsync();
        Task<FinancialLiteracyEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialLiteracyEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialLiteracyEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
