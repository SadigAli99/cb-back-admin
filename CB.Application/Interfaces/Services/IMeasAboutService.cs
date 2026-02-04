
using CB.Application.DTOs.MeasAbout;

namespace CB.Application.Interfaces.Services
{
    public interface IMeasAboutService
    {
        Task<bool> CreateOrUpdate(MeasAboutPostDTO dTO);
        Task<MeasAboutGetDTO?> GetFirst();
    }
}
