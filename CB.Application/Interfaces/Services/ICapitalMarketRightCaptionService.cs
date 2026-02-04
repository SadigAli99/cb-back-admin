
using CB.Application.DTOs.CapitalMarketRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketRightCaptionService
    {
        Task<bool> CreateOrUpdate(CapitalMarketRightCaptionPostDTO dTO);
        Task<CapitalMarketRightCaptionGetDTO?> GetFirst();
    }
}
