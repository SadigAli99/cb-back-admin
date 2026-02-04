
using CB.Application.DTOs.StaffArticleCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IStaffArticleCaptionService
    {
        Task<bool> CreateOrUpdate(StaffArticleCaptionPostDTO dTO);
        Task<StaffArticleCaptionGetDTO?> GetFirst();
    }
}
