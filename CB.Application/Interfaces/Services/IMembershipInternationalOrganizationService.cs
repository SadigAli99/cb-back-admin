
using CB.Application.DTOs.MembershipInternationalOrganization;

namespace CB.Application.Interfaces.Services
{
    public interface IMembershipInternationalOrganizationService
    {
        Task<List<MembershipInternationalOrganizationGetDTO>> GetAllAsync();
        Task<MembershipInternationalOrganizationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MembershipInternationalOrganizationCreateDTO dto);
        Task<bool> UpdateAsync(int id, MembershipInternationalOrganizationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
