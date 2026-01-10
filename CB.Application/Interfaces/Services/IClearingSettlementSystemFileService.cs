
using CB.Application.DTOs.ClearingSettlementSystemFile;

namespace CB.Application.Interfaces.Services
{
    public interface IClearingSettlementSystemFileService
    {
        Task<List<ClearingSettlementSystemFileGetDTO>> GetAllAsync();
        Task<ClearingSettlementSystemFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ClearingSettlementSystemFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, ClearingSettlementSystemFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
