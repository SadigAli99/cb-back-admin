
using CB.Application.DTOs.ReviewApplicationLink;

namespace CB.Application.Interfaces.Services
{
    public interface IReviewApplicationLinkService
    {
        Task<List<ReviewApplicationLinkGetDTO>> GetAllAsync();
        Task<ReviewApplicationLinkGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReviewApplicationLinkCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReviewApplicationLinkEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
