
using CB.Application.DTOs.MoneySignHistoryFeature;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignHistoryFeatureService
    {
        Task<bool> CreateOrUpdate(MoneySignHistoryFeaturePostDTO dTO);
        Task<MoneySignHistoryFeatureGetDTO?> GetFirst();
    }
}
