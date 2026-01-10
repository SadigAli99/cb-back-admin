
using CB.Application.DTOs.CBDC;

namespace CB.Application.Interfaces.Services
{
    public interface ICBDCService
    {
        Task<bool> CreateOrUpdate(CBDCPostDTO dTO);
        Task<CBDCGetDTO> GetFirst();
    }
}
