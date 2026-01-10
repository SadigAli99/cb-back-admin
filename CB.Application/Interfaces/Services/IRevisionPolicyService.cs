
using CB.Application.DTOs.RevisionPolicy;

namespace CB.Application.Interfaces.Services
{
    public interface IRevisionPolicyService
    {
        Task<bool> CreateOrUpdate(RevisionPolicyPostDTO dTO);
        Task<RevisionPolicyGetDTO> GetFirst();
    }
}
