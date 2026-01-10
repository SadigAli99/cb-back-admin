
using CB.Application.DTOs.MediaCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IMediaCaptionService
    {
        Task<bool> CreateOrUpdate(MediaCaptionPostDTO dTO);
        Task<MediaCaptionGetDTO> GetFirst();
    }
}
