
using CB.Application.DTOs.FinancialFlow;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialFlowService
    {
        Task<List<FinancialFlowGetDTO>> GetAllAsync();
        Task<FinancialFlowGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialFlowCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialFlowEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
