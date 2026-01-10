
using CB.Application.DTOs.Meas;

namespace CB.Application.Interfaces.Services
{
    public interface IMeasService
    {
        Task<List<MeasGetDTO>> GetAllAsync();
        Task<MeasGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MeasCreateDTO dto);
        Task<bool> UpdateAsync(int id, MeasEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
