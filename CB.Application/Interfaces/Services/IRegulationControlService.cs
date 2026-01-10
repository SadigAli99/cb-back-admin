
using CB.Application.DTOs.RegulationControl;

namespace CB.Application.Interfaces.Services
{
    public interface IRegulationControlService
    {
        Task<List<RegulationControlGetDTO>> GetAllAsync();
        Task<RegulationControlGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RegulationControlCreateDTO dto);
        Task<bool> UpdateAsync(int id, RegulationControlEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
