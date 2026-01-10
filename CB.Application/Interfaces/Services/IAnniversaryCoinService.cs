
using CB.Application.DTOs.AnniversaryCoin;

namespace CB.Application.Interfaces.Services
{
    public interface IAnniversaryCoinService
    {
        Task<bool> CreateOrUpdate(AnniversaryCoinPostDTO dTO);
        Task<AnniversaryCoinGetDTO> GetFirst();
    }
}
