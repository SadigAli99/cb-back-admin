
using CB.Application.DTOs.FinancialSearchSystemCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialSearchSystemCaptionService
    {
        Task<bool> CreateOrUpdate(FinancialSearchSystemCaptionPostDTO dTO);
        Task<FinancialSearchSystemCaptionGetDTO> GetFirst();
    }
}
