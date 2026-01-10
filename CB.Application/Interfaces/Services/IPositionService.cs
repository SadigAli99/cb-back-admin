using CB.Application.DTOs.Position;

namespace CB.Application.Interfaces.Services
{
    public interface IPositionService
    {
        Task<List<PositionGetDTO>> GetAllAsync();
        Task<PositionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PositionCreateDTO dto);
        Task<bool> UpdateAsync(int id, PositionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
