
using CB.Application.DTOs.LegalAct;

namespace CB.Application.Interfaces.Services
{
    public interface ILegalActService
    {
        Task<List<LegalActGetDTO>> GetAllAsync();
        Task<LegalActGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LegalActCreateDTO dto);
        Task<bool> UpdateAsync(int id, LegalActEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
