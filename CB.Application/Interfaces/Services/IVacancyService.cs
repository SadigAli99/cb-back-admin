
using CB.Application.DTOs.Vacancy;

namespace CB.Application.Interfaces.Services
{
    public interface IVacancyService
    {
        Task<List<VacancyGetDTO>> GetAllAsync();
        Task<VacancyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VacancyCreateDTO dto);
        Task<bool> UpdateAsync(int id, VacancyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
