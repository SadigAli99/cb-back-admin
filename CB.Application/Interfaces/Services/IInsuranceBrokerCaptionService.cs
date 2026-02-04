
using CB.Application.DTOs.InsuranceBrokerCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInsuranceBrokerCaptionService
    {
        Task<bool> CreateOrUpdate(InsuranceBrokerCaptionPostDTO dTO);
        Task<InsuranceBrokerCaptionGetDTO?> GetFirst();
    }
}
