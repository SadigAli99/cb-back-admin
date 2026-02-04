
using CB.Application.DTOs.BankCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IBankCaptionService
    {
        Task<bool> CreateOrUpdate(BankCaptionPostDTO dTO);
        Task<BankCaptionGetDTO?> GetFirst();
    }
}
