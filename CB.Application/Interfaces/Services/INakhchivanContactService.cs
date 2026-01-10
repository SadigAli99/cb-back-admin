
using CB.Application.DTOs.NakhchivanContact;

namespace CB.Application.Interfaces.Services
{
    public interface INakhchivanContactService
    {
        Task<bool> CreateOrUpdate(NakhchivanContactPostDTO dTO);
        Task<NakhchivanContactGetDTO> GetFirst();
    }
}
