
using CB.Application.DTOs.CustomerRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerRightCaptionService
    {
        Task<bool> CreateOrUpdate(CustomerRightCaptionPostDTO dTO);
        Task<CustomerRightCaptionGetDTO> GetFirst();
    }
}
