
using CB.Application.DTOs.MoneySignProtectionCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignProtectionCaptionService
    {
        Task<bool> CreateOrUpdate(MoneySignProtectionCaptionPostDTO dTO);
        Task<MoneySignProtectionCaptionGetDTO?> GetFirst();
    }
}
