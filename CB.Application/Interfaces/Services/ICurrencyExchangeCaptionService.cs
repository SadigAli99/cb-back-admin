
using CB.Application.DTOs.CurrencyExchangeCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyExchangeCaptionService
    {
        Task<bool> CreateOrUpdate(CurrencyExchangeCaptionPostDTO dTO);
        Task<CurrencyExchangeCaptionGetDTO> GetFirst();
    }
}
