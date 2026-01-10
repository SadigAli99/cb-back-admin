
using CB.Application.DTOs.InsurerPresidentAct;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerPresidentActService
    {
        Task<List<InsurerPresidentActGetDTO>> GetAllAsync();
        Task<InsurerPresidentActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerPresidentActCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerPresidentActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
