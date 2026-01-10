
using CB.Application.DTOs.LossAdjuster;

namespace CB.Application.Interfaces.Services
{
    public interface ILossAdjusterService
    {
        Task<List<LossAdjusterGetDTO>> GetAllAsync();
        Task<LossAdjusterGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LossAdjusterCreateDTO dto);
        Task<bool> UpdateAsync(int id, LossAdjusterEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
