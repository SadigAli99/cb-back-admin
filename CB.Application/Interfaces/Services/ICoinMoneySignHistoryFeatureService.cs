
using CB.Application.DTOs.CoinMoneySignHistoryFeature;

namespace CB.Application.Interfaces.Services
{
    public interface ICoinMoneySignHistoryFeatureService
    {
        Task<bool> CreateOrUpdate(CoinMoneySignHistoryFeaturePostDTO dTO);
        Task<CoinMoneySignHistoryFeatureGetDTO?> GetFirst();
    }
}
