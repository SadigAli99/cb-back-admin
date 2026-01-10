
using CB.Application.DTOs.CurrencyRegulationRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyRegulationRightCaptionService
    {
        Task<bool> CreateOrUpdate(CurrencyRegulationRightCaptionPostDTO dTO);
        Task<CurrencyRegulationRightCaptionGetDTO> GetFirst();
    }
}
