
using CB.Application.DTOs.QualificationCertificateCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IQualificationCertificateCaptionService
    {
        Task<bool> CreateOrUpdate(QualificationCertificateCaptionPostDTO dTO);
        Task<QualificationCertificateCaptionGetDTO?> GetFirst();
    }
}
