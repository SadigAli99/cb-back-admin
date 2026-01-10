
using CB.Application.DTOs.FinancialLiteracyPortalCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialLiteracyPortalCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialLiteracyPortalCaptionPostDTO dTO);
        Task<FinancialLiteracyPortalCaptionGetDTO> GetFirst();
    }
}
