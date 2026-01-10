
using CB.Application.DTOs.FinancialLiteracyPortal;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialLiteracyPortalService
    {
        Task<List<FinancialLiteracyPortalGetDTO>> GetAllAsync();
        Task<FinancialLiteracyPortalGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialLiteracyPortalCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialLiteracyPortalEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
