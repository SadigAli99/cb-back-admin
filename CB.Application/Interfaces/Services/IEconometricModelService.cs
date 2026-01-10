
using CB.Application.DTOs.EconometricModel;

namespace CB.Application.Interfaces.Services
{
    public interface IEconometricModelService
    {
        Task<bool> CreateOrUpdate(EconometricModelPostDTO dTO);
        Task<EconometricModelGetDTO> GetFirst();
    }
}
