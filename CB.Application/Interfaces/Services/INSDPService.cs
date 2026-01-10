
using CB.Application.DTOs.NSDP;

namespace CB.Application.Interfaces.Services
{
    public interface INSDPService
    {
        Task<List<NSDPGetDTO>> GetAllAsync();
        Task<NSDPGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NSDPCreateDTO dto);
        Task<bool> UpdateAsync(int id, NSDPEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
