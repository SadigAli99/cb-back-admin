
using CB.Application.DTOs.Logo;

namespace CB.Application.Interfaces.Services
{
    public interface ILogoService
    {
        Task<bool> CreateOrUpdate(LogoPostDTO dTO);
        Task<LogoGetDTO> GetFirst();
    }
}
