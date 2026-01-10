
using CB.Application.DTOs.StateProgramCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IStateProgramCaptionService
    {
        Task<bool> CreateOrUpdate(StateProgramCaptionPostDTO dTO);
        Task<StateProgramCaptionGetDTO> GetFirst();
    }
}
