
using CB.Application.DTOs.MeasFile;

namespace CB.Application.Interfaces.Services
{
    public interface IMeasFileService
    {
        Task<List<MeasFileGetDTO>> GetAllAsync();
        Task<MeasFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MeasFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, MeasFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
