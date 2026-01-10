
using CB.Application.DTOs.NakhchivanPublication;

namespace CB.Application.Interfaces.Services
{
    public interface INakhchivanPublicationService
    {
        Task<List<NakhchivanPublicationGetDTO>> GetAllAsync();
        Task<NakhchivanPublicationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(NakhchivanPublicationCreateDTO dto);
        Task<bool> UpdateAsync(int id, NakhchivanPublicationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
