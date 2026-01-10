
using CB.Application.DTOs.OtherMinisterAct;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherMinisterActService
    {
        Task<List<OtherMinisterActGetDTO>> GetAllAsync();
        Task<OtherMinisterActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OtherMinisterActCreateDTO dto);
        Task<bool> UpdateAsync(int id, OtherMinisterActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
