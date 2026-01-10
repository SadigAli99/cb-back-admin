
using CB.Application.DTOs.Regulation;

namespace CB.Application.Interfaces.Services
{
    public interface IRegulationService
    {
        Task<List<RegulationGetDTO>> GetAllAsync();
        Task<RegulationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RegulationCreateDTO dto);
        Task<bool> UpdateAsync(int id, RegulationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
