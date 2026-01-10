
using CB.Application.DTOs.About;

namespace CB.Application.Interfaces.Services
{
    public interface IAboutService
    {
        Task<bool> CreateOrUpdate(AboutPostDTO dTO);
        Task<AboutGetDTO> GetFirst();
    }
}
