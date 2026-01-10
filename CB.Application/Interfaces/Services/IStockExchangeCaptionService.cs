
using CB.Application.DTOs.StockExchangeCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IStockExchangeCaptionService
    {
        Task<bool> CreateOrUpdate(StockExchangeCaptionPostDTO dTO);
        Task<StockExchangeCaptionGetDTO> GetFirst();
    }
}
