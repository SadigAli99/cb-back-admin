
using CB.Application.DTOs.FinancialLiteracyCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialLiteracyCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialLiteracyCaptionPostDTO dTO);
        Task<FinancialLiteracyCaptionGetDTO> GetFirst();
    }
}
