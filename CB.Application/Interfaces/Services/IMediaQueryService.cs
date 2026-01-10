
using CB.Application.DTOs.MediaQuery;

namespace CB.Application.Interfaces.Services
{
    public interface IMediaQueryService
    {
        Task<bool> CreateOrUpdate(MediaQueryPostDTO dTO);
        Task<MediaQueryGetDTO> GetFirst();
    }
}
