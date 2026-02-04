
using CB.Application.DTOs.PublicationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IPublicationCaptionService
    {
        Task<bool> CreateOrUpdate(PublicationCaptionPostDTO dTO);
        Task<PublicationCaptionGetDTO?> GetFirst();
    }
}
