
using CB.Application.DTOs.ActuaryCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IActuaryCaptionService
    {
        Task<bool> CreateOrUpdate(ActuaryCaptionPostDTO dTO);
        Task<ActuaryCaptionGetDTO?> GetFirst();
    }
}
