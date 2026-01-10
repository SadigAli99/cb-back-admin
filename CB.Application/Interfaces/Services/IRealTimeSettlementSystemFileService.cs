
using CB.Application.DTOs.RealTimeSettlementSystemFile;

namespace CB.Application.Interfaces.Services
{
    public interface IRealTimeSettlementSystemFileService
    {
        Task<List<RealTimeSettlementSystemFileGetDTO>> GetAllAsync();
        Task<RealTimeSettlementSystemFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RealTimeSettlementSystemFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, RealTimeSettlementSystemFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
