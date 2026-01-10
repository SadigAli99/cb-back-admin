
using CB.Application.DTOs.FraudStatisticCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IFraudStatisticCaptionService
    {
        Task<bool> CreateOrUpdate(FraudStatisticCaptionPostDTO dTO);
        Task<FraudStatisticCaptionGetDTO> GetFirst();
    }
}
