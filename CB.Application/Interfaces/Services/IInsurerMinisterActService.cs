
using CB.Application.DTOs.InsurerMinisterAct;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerMinisterActService
    {
        Task<List<InsurerMinisterActGetDTO>> GetAllAsync();
        Task<InsurerMinisterActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerMinisterActCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerMinisterActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
