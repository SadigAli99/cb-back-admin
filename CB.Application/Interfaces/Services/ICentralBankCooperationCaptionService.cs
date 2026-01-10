
using CB.Application.DTOs.CentralBankCooperationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICentralBankCooperationCaptionService
    {
        Task<bool> CreateOrUpdate(CentralBankCooperationCaptionPostDTO dTO);
        Task<CentralBankCooperationCaptionGetDTO> GetFirst();
    }
}
