
using CB.Application.DTOs.Methodology;

namespace CB.Application.Interfaces.Services
{
    public interface IMethodologyService
    {
        Task<List<MethodologyGetDTO>> GetAllAsync();
        Task<MethodologyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MethodologyCreateDTO dto);
        Task<bool> UpdateAsync(int id, MethodologyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
