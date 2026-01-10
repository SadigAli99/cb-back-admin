using CB.Application.DTOs.Social;

namespace CB.Application.Interfaces.Services
{
    public interface ISocialService
    {
        Task<List<SocialGetDTO>> GetAllAsync();
        Task<SocialGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SocialCreateDTO dto);
        Task<bool> UpdateAsync(int id, SocialEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
