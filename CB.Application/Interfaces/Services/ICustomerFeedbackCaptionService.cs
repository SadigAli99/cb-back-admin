
using CB.Application.DTOs.CustomerFeedbackCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerFeedbackCaptionService
    {
        Task<bool> CreateOrUpdate(CustomerFeedbackCaptionPostDTO dTO);
        Task<CustomerFeedbackCaptionGetDTO> GetFirst();
    }
}
