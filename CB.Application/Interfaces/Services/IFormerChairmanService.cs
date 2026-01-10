
using CB.Application.DTOs.FormerChairman;

namespace CB.Application.Interfaces.Services
{
    public interface IFormerChairmanService
    {
        Task<List<FormerChairmanGetDTO>> GetAllAsync();
        Task<FormerChairmanGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FormerChairmanCreateDTO dto);
        Task<bool> UpdateAsync(int id, FormerChairmanEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
