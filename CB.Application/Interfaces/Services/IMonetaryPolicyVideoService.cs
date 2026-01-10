
using CB.Application.DTOs.MonetaryPolicyVideo;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryPolicyVideoService
    {
        Task<bool> CreateOrUpdate(MonetaryPolicyVideoPostDTO dTO);
        Task<MonetaryPolicyVideoGetDTO> GetFirst();
    }
}
