
using CB.Application.DTOs.CustomerProposal;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerProposalService
    {
        Task<bool> CreateOrUpdate(CustomerProposalPostDTO dTO);
        Task<CustomerProposalGetDTO> GetFirst();
    }
}
