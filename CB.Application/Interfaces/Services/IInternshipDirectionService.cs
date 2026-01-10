
using CB.Application.DTOs.InternshipDirection;

namespace CB.Application.Interfaces.Services
{
    public interface IInternshipDirectionService
    {
        Task<List<InternshipDirectionGetDTO>> GetAllAsync();
        Task<InternshipDirectionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InternshipDirectionCreateDTO dto);
        Task<bool> UpdateAsync(int id, InternshipDirectionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
