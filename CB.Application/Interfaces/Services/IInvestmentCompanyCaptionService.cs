
using CB.Application.DTOs.InvestmentCompanyCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInvestmentCompanyCaptionService
    {
        Task<bool> CreateOrUpdate(InvestmentCompanyCaptionPostDTO dTO);
        Task<InvestmentCompanyCaptionGetDTO> GetFirst();
    }
}
