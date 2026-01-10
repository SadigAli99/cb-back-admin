
using CB.Application.DTOs.InstantPaymentOrganization;

namespace CB.Application.Interfaces.Services
{
    public interface IInstantPaymentOrganizationService
    {
        Task<List<InstantPaymentOrganizationGetDTO>> GetAllAsync();
        Task<InstantPaymentOrganizationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InstantPaymentOrganizationCreateDTO dto);
        Task<bool> UpdateAsync(int id, InstantPaymentOrganizationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
