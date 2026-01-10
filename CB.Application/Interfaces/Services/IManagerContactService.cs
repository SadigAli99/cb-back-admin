
using CB.Application.DTOs.ManagerContact;

namespace CB.Application.Interfaces.Services
{
    public interface IManagerContactService
    {
        Task<List<ManagerContactGetDTO>> GetAllAsync(int? id);
        Task<ManagerContactGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ManagerContactCreateDTO dto);
        Task<bool> UpdateAsync(int id, ManagerContactEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
