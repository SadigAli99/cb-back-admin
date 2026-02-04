
using CB.Application.DTOs.DepositorySystem;

namespace CB.Application.Interfaces.Services
{
    public interface IDepositorySystemService
    {
        Task<bool> CreateOrUpdate(DepositorySystemPostDTO dTO);
        Task<DepositorySystemGetDTO?> GetFirst();
    }
}
