
using CB.Application.DTOs.FaqVideo;

namespace CB.Application.Interfaces.Services
{
    public interface IFaqVideoService
    {
        Task<bool> CreateOrUpdate(FaqVideoPostDTO dTO);
        Task<FaqVideoGetDTO?> GetFirst();
    }
}
