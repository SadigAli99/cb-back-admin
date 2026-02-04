
using CB.Application.DTOs.InfographicDisclosureGraphic;

namespace CB.Application.Interfaces.Services
{
    public interface IInfographicDisclosureGraphicService
    {
        Task<bool> CreateOrUpdate(InfographicDisclosureGraphicPostDTO dTO);
        Task<InfographicDisclosureGraphicGetDTO?> GetFirst();
    }
}
