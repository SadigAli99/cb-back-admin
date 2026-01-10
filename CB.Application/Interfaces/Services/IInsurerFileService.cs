
using CB.Application.DTOs.InsurerFile;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerFileService
    {
        Task<List<InsurerFileGetDTO>> GetAllAsync();
        Task<InsurerFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
