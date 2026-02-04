
using CB.Application.DTOs.OpenBanking;

namespace CB.Application.Interfaces.Services
{
    public interface IOpenBankingService
    {
        Task<bool> CreateOrUpdate(OpenBankingPostDTO dTO);
        Task<OpenBankingGetDTO?> GetFirst();
    }
}
