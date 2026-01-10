
using CB.Application.DTOs.ClearingSettlementSystem;

namespace CB.Application.Interfaces.Services
{
    public interface IClearingSettlementSystemService
    {
        Task<List<ClearingSettlementSystemGetDTO>> GetAllAsync();
        Task<ClearingSettlementSystemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ClearingSettlementSystemCreateDTO dto);
        Task<bool> UpdateAsync(int id, ClearingSettlementSystemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
