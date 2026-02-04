
using CB.Application.DTOs.NonBankCaption;

namespace CB.Application.Interfaces.Services
{
    public interface INonBankCaptionService
    {
        Task<bool> CreateOrUpdate(NonBankCaptionPostDTO dTO);
        Task<NonBankCaptionGetDTO?> GetFirst();
    }
}
