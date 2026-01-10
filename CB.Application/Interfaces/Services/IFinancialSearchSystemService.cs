
using CB.Application.DTOs.FinancialSearchSystem;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialSearchSystemService
    {
        Task<List<FinancialSearchSystemGetDTO>> GetAllAsync();
        Task<FinancialSearchSystemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FinancialSearchSystemCreateDTO dto);
        Task<bool> UpdateAsync(int id, FinancialSearchSystemEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
