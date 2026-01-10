
using CB.Application.DTOs.ManagerDetail;

namespace CB.Application.Interfaces.Services
{
    public interface IManagerDetailService
    {
        Task<List<ManagerDetailGetDTO>> GetAllAsync(int? id);
        Task<ManagerDetailGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ManagerDetailCreateDTO dto);
        Task<bool> UpdateAsync(int id, ManagerDetailEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
