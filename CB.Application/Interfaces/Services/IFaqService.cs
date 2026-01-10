
using CB.Application.DTOs.Faq;

namespace CB.Application.Interfaces.Services
{
    public interface IFaqService
    {
        Task<List<FaqGetDTO>> GetAllAsync();
        Task<FaqGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FaqCreateDTO dto);
        Task<bool> UpdateAsync(int id, FaqEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
