
using CB.Application.DTOs.Manager;

namespace CB.Application.Interfaces.Services
{
    public interface IManagerService
    {
        Task<List<ManagerGetDTO>> GetAllAsync();
        Task<ManagerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ManagerCreateDTO dto);
        Task<bool> UpdateAsync(int id, ManagerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
