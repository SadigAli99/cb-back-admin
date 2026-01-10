
using CB.Application.DTOs.MonetaryPolicyDecision;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyDecisionService
    {
        Task<List<MonetaryPolicyDecisionGetDTO>> GetAllAsync();
        Task<MonetaryPolicyDecisionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryPolicyDecisionCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryPolicyDecisionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
