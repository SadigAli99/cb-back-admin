
using CB.Application.DTOs.MethodologyExplain;

namespace CB.Application.Interfaces.Services
{
    public interface IMethodologyExplainService
    {
        Task<bool> CreateOrUpdate(MethodologyExplainPostDTO dTO);
        Task<MethodologyExplainGetDTO?> GetFirst();
    }
}
