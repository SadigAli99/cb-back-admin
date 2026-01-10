
using CB.Application.DTOs.OtherLaw;

namespace CB.Application.Interfaces.Services
{
    public interface IOtherLawService
    {
        Task<List<OtherLawGetDTO>> GetAllAsync();
        Task<OtherLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OtherLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, OtherLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
