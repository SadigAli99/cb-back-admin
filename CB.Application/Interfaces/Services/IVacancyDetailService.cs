
using CB.Application.DTOs.VacancyDetail;

namespace CB.Application.Interfaces.Services
{
    public interface IVacancyDetailService
    {
        Task<List<VacancyDetailGetDTO>> GetAllAsync(int? id);
        Task<VacancyDetailGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VacancyDetailCreateDTO dto);
        Task<bool> UpdateAsync(int id, VacancyDetailEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
