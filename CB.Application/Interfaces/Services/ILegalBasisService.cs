
using CB.Application.DTOs.LegalBasis;

namespace CB.Application.Interfaces.Services
{
    public interface ILegalBasisService
    {
        Task<List<LegalBasisGetDTO>> GetAllAsync();
        Task<LegalBasisGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LegalBasisCreateDTO dto);
        Task<bool> UpdateAsync(int id, LegalBasisEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
