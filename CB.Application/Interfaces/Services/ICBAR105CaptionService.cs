
using CB.Application.DTOs.CBAR105Caption;

namespace CB.Application.Interfaces.Services
{
    public interface ICBAR105CaptionService
    {
        Task<bool> CreateOrUpdate(CBAR105CaptionPostDTO dTO);
        Task<CBAR105CaptionGetDTO?> GetFirst();
    }
}
