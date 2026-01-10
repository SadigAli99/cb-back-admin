
using CB.Application.DTOs.VirtualActive;

namespace CB.Application.Interfaces.Services
{
    public interface IVirtualActiveService
    {
        Task<bool> CreateOrUpdate(VirtualActivePostDTO dTO);
        Task<VirtualActiveGetDTO> GetFirst();
    }
}
