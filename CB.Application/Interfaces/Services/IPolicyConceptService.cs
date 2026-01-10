
using CB.Application.DTOs.PolicyConcept;

namespace CB.Application.Interfaces.Services
{
    public interface IPolicyConceptService
    {
        Task<List<PolicyConceptGetDTO>> GetAllAsync();
        Task<PolicyConceptGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PolicyConceptCreateDTO dto);
        Task<bool> UpdateAsync(int id, PolicyConceptEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
