using CB.Application.DTOs.Statute;

namespace CB.Application.Interfaces.Services
{
    public interface IStatuteService
    {
        Task<List<StatuteGetDTO>> GetAllAsync();
        Task<StatuteGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(StatuteCreateDTO dto);
        Task<bool> UpdateAsync(int id, StatuteEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
