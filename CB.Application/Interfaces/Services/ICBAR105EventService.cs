
using CB.Application.DTOs.CBAR105Event;

namespace CB.Application.Interfaces.Services
{
    public interface ICBAR105EventService
    {
        Task<bool> CreateOrUpdate(CBAR105EventPostDTO dTO);
        Task<CBAR105EventGetDTO?> GetFirst();
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
