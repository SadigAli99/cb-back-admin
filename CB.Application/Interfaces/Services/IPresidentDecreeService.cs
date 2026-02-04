
using CB.Application.DTOs.PresidentDecree;

namespace CB.Application.Interfaces.Services
{
    public interface IPresidentDecreeService
    {
        Task<bool> CreateOrUpdate(PresidentDecreePostDTO dTO);
        Task<PresidentDecreeGetDTO?> GetFirst();
    }
}
