
using CB.Application.DTOs.InformationIssuerCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationIssuerCaptionService
    {
        Task<bool> CreateOrUpdate(InformationIssuerCaptionPostDTO dTO);
        Task<InformationIssuerCaptionGetDTO> GetFirst();
    }
}
