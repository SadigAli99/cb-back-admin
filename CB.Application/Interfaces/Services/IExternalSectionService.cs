
using CB.Application.DTOs.ExternalSection;

namespace CB.Application.Interfaces.Services
{
    public interface IExternalSectionService
    {
        Task<List<ExternalSectionGetDTO>> GetAllAsync();
        Task<ExternalSectionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ExternalSectionCreateDTO dto);
        Task<bool> UpdateAsync(int id, ExternalSectionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
