
using CB.Application.DTOs.DirectorContact;

namespace CB.Application.Interfaces.Services
{
    public interface IDirectorContactService
    {
        Task<List<DirectorContactGetDTO>> GetAllAsync(int? id);
        Task<DirectorContactGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DirectorContactCreateDTO dto);
        Task<bool> UpdateAsync(int id, DirectorContactEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
