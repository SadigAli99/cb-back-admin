
using CB.Application.DTOs.CreditUnionCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditUnionCaptionService
    {
        Task<bool> CreateOrUpdate(CreditUnionCaptionPostDTO dTO);
        Task<CreditUnionCaptionGetDTO?> GetFirst();
    }
}
