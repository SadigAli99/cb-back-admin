
using CB.Application.DTOs.MonetaryPolicyGraphic;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyGraphicService
    {
        Task<List<MonetaryPolicyGraphicGetDTO>> GetAllAsync();
        Task<MonetaryPolicyGraphicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryPolicyGraphicCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryPolicyGraphicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
