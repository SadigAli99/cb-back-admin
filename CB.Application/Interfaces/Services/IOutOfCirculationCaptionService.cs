
using CB.Application.DTOs.OutOfCirculationCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCirculationCaptionService
    {
        Task<bool> CreateOrUpdate(OutOfCirculationCaptionPostDTO dTO);
        Task<OutOfCirculationCaptionGetDTO> GetFirst();
    }
}
