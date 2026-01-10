
using CB.Application.DTOs.InstantPaymentSystem;

namespace CB.Application.Interfaces.Services
{
    public interface IInstantPaymentSystemService
    {
        Task<bool> CreateOrUpdate(InstantPaymentSystemPostDTO dTO);
        Task<InstantPaymentSystemGetDTO> GetFirst();
    }
}
