
using CB.Application.DTOs.CustomEditingMode;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomEditingModeService
    {
        Task<bool> CreateOrUpdate(CustomEditingModePostDTO dTO);
        Task<CustomEditingModeGetDTO> GetFirst();
    }
}
