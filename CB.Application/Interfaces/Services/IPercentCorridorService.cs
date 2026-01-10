using CB.Application.DTOs.PercentCorridor;

namespace CB.Application.Interfaces.Services
{
    public interface IPercentCorridorService
    {
        Task<List<PercentCorridorGetDTO>> GetAllAsync();
        Task<PercentCorridorGetDTO?> GetByIdAsync(int id);
        Task<PercentCorridorEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(PercentCorridorCreateDTO dto);
        Task<bool> UpdateAsync(int id, PercentCorridorEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
