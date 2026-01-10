
using CB.Application.DTOs.ReviewApplicationVideo;

namespace CB.Application.Interfaces.Services
{
    public interface IReviewApplicationVideoService
    {
        Task<List<ReviewApplicationVideoGetDTO>> GetAllAsync();
        Task<ReviewApplicationVideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReviewApplicationVideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReviewApplicationVideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
