
using CB.Application.DTOs.FinancialDevelopment;

namespace CB.Application.Interfaces.Services
{
    public interface IFinancialDevelopmentService
    {
        Task<bool> CreateOrUpdate(FinancialDevelopmentPostDTO dTO);
        Task<FinancialDevelopmentGetDTO?> GetFirst();
    }
}
