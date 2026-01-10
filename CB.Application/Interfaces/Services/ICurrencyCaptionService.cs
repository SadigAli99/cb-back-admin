
using CB.Application.DTOs.CurrencyCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyCaptionService
    {
        Task<bool> CreateOrUpdate(CurrencyCaptionPostDTO dTO);
        Task<CurrencyCaptionGetDTO> GetFirst();
    }
}
