
using CB.Application.DTOs.InsurerLaw;

namespace CB.Application.Interfaces.Services
{
    public interface IInsurerLawService
    {
        Task<List<InsurerLawGetDTO>> GetAllAsync();
        Task<InsurerLawGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsurerLawCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsurerLawEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
