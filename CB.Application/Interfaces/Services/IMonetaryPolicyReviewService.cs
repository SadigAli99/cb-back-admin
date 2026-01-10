
using CB.Application.DTOs.MonetaryPolicyReview;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyReviewService
    {
        Task<List<MonetaryPolicyReviewGetDTO>> GetAllAsync();
        Task<MonetaryPolicyReviewGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryPolicyReviewCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryPolicyReviewEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
