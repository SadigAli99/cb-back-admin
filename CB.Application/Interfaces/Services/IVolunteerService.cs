
using CB.Application.DTOs.Volunteer;

namespace CB.Application.Interfaces.Services
{
    public interface IVolunteerService
    {
        Task<List<VolunteerGetDTO>> GetAllAsync();
        Task<VolunteerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VolunteerCreateDTO dto);
        Task<bool> UpdateAsync(int id, VolunteerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
