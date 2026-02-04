
using CB.Application.DTOs.ElectronicMoneyInstitutionCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IElectronicMoneyInstitutionCaptionService
    {
        Task<bool> CreateOrUpdate(ElectronicMoneyInstitutionCaptionPostDTO dTO);
        Task<ElectronicMoneyInstitutionCaptionGetDTO?> GetFirst();
    }
}
