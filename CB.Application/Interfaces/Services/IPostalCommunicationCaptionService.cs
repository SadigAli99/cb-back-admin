
using CB.Application.DTOs.PostalCommunicationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPostalCommunicationCaptionService
    {
        Task<bool> CreateOrUpdate(PostalCommunicationCaptionPostDTO dTO);
        Task<PostalCommunicationCaptionGetDTO> GetFirst();
    }
}
