using CB.Application.DTOs.StaffArticle;

namespace CB.Application.Interfaces.Services
{
    public interface IStaffArticleService
    {
        Task<List<StaffArticleGetDTO>> GetAllAsync();
        Task<StaffArticleGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StaffArticleCreateDTO dto);
        Task<bool> UpdateAsync(int id, StaffArticleEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
