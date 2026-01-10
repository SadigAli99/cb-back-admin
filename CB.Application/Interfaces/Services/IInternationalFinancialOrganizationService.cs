
using CB.Application.DTOs.InternationalFinancialOrganization;

namespace CB.Application.Interfaces.Services
{
    public interface IInternationalFinancialOrganizationService
    {
        Task<bool> CreateOrUpdate(InternationalFinancialOrganizationPostDTO dTO);
        Task<InternationalFinancialOrganizationGetDTO> GetFirst();
    }
}
