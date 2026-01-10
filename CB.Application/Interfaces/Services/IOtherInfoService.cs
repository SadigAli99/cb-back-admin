
using CB.Application.DTOs.OtherInfo;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherInfoService
    {
        Task<List<OtherInfoGetDTO>> GetAllAsync();
        Task<OtherInfoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OtherInfoCreateDTO dto);
        Task<bool> UpdateAsync(int id, OtherInfoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
