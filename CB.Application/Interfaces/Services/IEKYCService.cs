
using CB.Application.DTOs.EKYC;

namespace CB.Application.Interfaces.Services
{
    public interface IEKYCService
    {
        Task<bool> CreateOrUpdate(EKYCPostDTO dTO);
        Task<EKYCGetDTO?> GetFirst();
    }
}
