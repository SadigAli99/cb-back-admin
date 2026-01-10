
using CB.Application.DTOs.MonetaryPolicyInstrument;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyInstrumentService
    {
        Task<List<MonetaryPolicyInstrumentGetDTO>> GetAllAsync();
        Task<MonetaryPolicyInstrumentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryPolicyInstrumentCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryPolicyInstrumentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
