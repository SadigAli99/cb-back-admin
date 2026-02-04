
using CB.Application.DTOs.BankInvestmentCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IBankInvestmentCaptionService
    {
        Task<bool> CreateOrUpdate(BankInvestmentCaptionPostDTO dTO);
        Task<BankInvestmentCaptionGetDTO?> GetFirst();
    }
}
