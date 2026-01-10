
using CB.Application.DTOs.PageDetail;

namespace CB.Application.Interfaces.Services
{
    public interface IPageDetailService
    {
        Task<List<PageDetailGetDTO>> GetAllAsync();
        Task<PageDetailGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PageDetailCreateDTO dto);
        Task<bool> UpdateAsync(int id, PageDetailEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
