
using CB.Application.DTOs.CreditInstitutionRightCaption;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditInstitutionRightCaptionService
    {
        Task<bool> CreateOrUpdate(CreditInstitutionRightCaptionPostDTO dTO);
        Task<CreditInstitutionRightCaptionGetDTO> GetFirst();
    }
}
