
using CB.Application.DTOs.LegalActMethodology;

namespace CB.Application.Interfaces.Services
{
    public interface ILegalActMethodologyService
    {
        Task<List<LegalActMethodologyGetDTO>> GetAllAsync();
        Task<LegalActMethodologyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LegalActMethodologyCreateDTO dto);
        Task<bool> UpdateAsync(int id, LegalActMethodologyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
