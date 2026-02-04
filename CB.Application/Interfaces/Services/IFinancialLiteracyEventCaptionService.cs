
using CB.Application.DTOs.FinancialLiteracyEventCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialLiteracyEventCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialLiteracyEventCaptionPostDTO dTO);
        Task<FinancialLiteracyEventCaptionGetDTO?> GetFirst();
    }
}
