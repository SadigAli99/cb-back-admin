
using CB.Application.DTOs.RealTimeSettlementSystem;

namespace CB.Application.Interfaces.Services
{
    public interface IRealTimeSettlementSystemService
    {
        Task<List<RealTimeSettlementSystemGetDTO>> GetAllAsync();
        Task<RealTimeSettlementSystemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RealTimeSettlementSystemCreateDTO dto);
        Task<bool> UpdateAsync(int id, RealTimeSettlementSystemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
