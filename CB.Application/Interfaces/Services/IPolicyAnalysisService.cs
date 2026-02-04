
using CB.Application.DTOs.PolicyAnalysis;

namespace CB.Application.Interfaces.Services
{
    public interface IPolicyAnalysisService
    {
        Task<bool> CreateOrUpdate(PolicyAnalysisPostDTO dTO);
        Task<PolicyAnalysisGetDTO?> GetFirst();
    }
}
