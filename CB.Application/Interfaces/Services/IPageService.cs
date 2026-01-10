using CB.Application.DTOs.Page;

namespace CB.Application.Interfaces.Services
{
    public interface IPageService
    {
        Task<List<PageGetDTO>> GetAllAsync();
        Task<PageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PageCreateDTO dto);
        Task<bool> UpdateAsync(int id, PageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
