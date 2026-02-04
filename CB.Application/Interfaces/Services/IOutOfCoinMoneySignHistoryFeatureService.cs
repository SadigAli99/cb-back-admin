
using CB.Application.DTOs.OutOfCoinMoneySignHistoryFeature;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCoinMoneySignHistoryFeatureService
    {
        Task<bool> CreateOrUpdate(OutOfCoinMoneySignHistoryFeaturePostDTO dTO);
        Task<OutOfCoinMoneySignHistoryFeatureGetDTO?> GetFirst();
    }
}
