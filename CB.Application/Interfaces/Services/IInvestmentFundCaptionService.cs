
using CB.Application.DTOs.InvestmentFundCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentFundCaptionService
    {
        Task<bool> CreateOrUpdate(InvestmentFundCaptionPostDTO dTO);
        Task<InvestmentFundCaptionGetDTO> GetFirst();
    }
}
