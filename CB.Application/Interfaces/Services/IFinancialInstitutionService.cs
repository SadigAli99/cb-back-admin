
using CB.Application.DTOs.FinancialInstitution;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialInstitutionService
    {
        Task<bool> CreateOrUpdate(FinancialInstitutionPostDTO dTO);
        Task<FinancialInstitutionGetDTO?> GetFirst();
    }
}
