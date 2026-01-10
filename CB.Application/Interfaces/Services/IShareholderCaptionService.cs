
using CB.Application.DTOs.ShareholderCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IShareholderCaptionService
    {
        Task<bool> CreateOrUpdate(ShareholderCaptionPostDTO dTO);
        Task<ShareholderCaptionGetDTO> GetFirst();
    }
}
