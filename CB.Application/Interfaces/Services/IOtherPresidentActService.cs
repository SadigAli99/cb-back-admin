
using CB.Application.DTOs.OtherPresidentAct;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherPresidentActService
    {
        Task<List<OtherPresidentActGetDTO>> GetAllAsync();
        Task<OtherPresidentActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OtherPresidentActCreateDTO dto);
        Task<bool> UpdateAsync(int id, OtherPresidentActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
