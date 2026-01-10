
using CB.Application.DTOs.MonetaryPolicyDirection;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyDirectionService
    {
        Task<List<MonetaryPolicyDirectionGetDTO>> GetAllAsync();
        Task<MonetaryPolicyDirectionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryPolicyDirectionCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryPolicyDirectionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
