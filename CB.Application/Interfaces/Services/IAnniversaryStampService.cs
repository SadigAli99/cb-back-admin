
using CB.Application.DTOs.AnniversaryStamp;

namespace CB.Application.Interfaces.Services
{
    public interface IAnniversaryStampService
    {
        Task<bool> CreateOrUpdate(AnniversaryStampPostDTO dTO);
        Task<AnniversaryStampGetDTO> GetFirst();
    }
}
