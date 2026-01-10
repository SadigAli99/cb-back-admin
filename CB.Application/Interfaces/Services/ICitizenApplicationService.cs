using CB.Application.DTOs.CitizenApplication;

namespace CB.Application.Interfaces.Services
{
    public interface ICitizenApplicationService
    {
        Task<List<CitizenApplicationGetDTO>> GetAllAsync();
        Task<CitizenApplicationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CitizenApplicationCreateDTO dto);
        Task<bool> UpdateAsync(int id, CitizenApplicationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
