
using CB.Application.DTOs.QualificationCertificate;

namespace CB.Application.Interfaces.Services
{
    public interface IQualificationCertificateService
    {
        Task<List<QualificationCertificateGetDTO>> GetAllAsync();
        Task<QualificationCertificateGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(QualificationCertificateCreateDTO dto);
        Task<bool> UpdateAsync(int id, QualificationCertificateEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
