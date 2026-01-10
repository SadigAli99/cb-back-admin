using CB.Application.DTOs.ReviewApplication;

namespace CB.Application.Interfaces.Services
{
    public interface IReviewApplicationService
    {
        Task<List<ReviewApplicationGetDTO>> GetAllAsync();
        Task<ReviewApplicationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReviewApplicationCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReviewApplicationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
