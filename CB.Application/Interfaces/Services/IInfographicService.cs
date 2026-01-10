
using CB.Application.DTOs.Infographic;

namespace CB.Application.Interfaces.Services
{
    public interface IInfographicService
    {
        Task<List<InfographicGetDTO>> GetAllAsync();
        Task<InfographicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InfographicCreateDTO dto);
        Task<bool> UpdateAsync(int id, InfographicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
