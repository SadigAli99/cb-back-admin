
using CB.Application.DTOs.ForeignInsuranceBrokerCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IForeignInsuranceBrokerCaptionService
    {
        Task<bool> CreateOrUpdate(ForeignInsuranceBrokerCaptionPostDTO dTO);
        Task<ForeignInsuranceBrokerCaptionGetDTO?> GetFirst();
    }
}
