
using CB.Application.DTOs.CareerCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICareerCaptionService
    {
        Task<bool> CreateOrUpdate(CareerCaptionPostDTO dTO);
        Task<CareerCaptionGetDTO> GetFirst();
    }
}
