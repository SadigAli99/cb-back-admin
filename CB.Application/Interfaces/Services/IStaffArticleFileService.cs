
using CB.Application.DTOs.StaffArticleFile;

namespace CB.Application.Interfaces.Services
{
    public interface IStaffArticleFileService
    {
        Task<List<StaffArticleFileGetDTO>> GetAllAsync();
        Task<StaffArticleFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StaffArticleFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, StaffArticleFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
