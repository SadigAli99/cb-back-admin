
using CB.Application.DTOs.ReviewApplicationFile;

namespace CB.Application.Interfaces.Services
{
    public interface IReviewApplicationFileService
    {
        Task<List<ReviewApplicationFileGetDTO>> GetAllAsync();
        Task<ReviewApplicationFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReviewApplicationFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReviewApplicationFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
