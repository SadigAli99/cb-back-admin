
using CB.Application.DTOs.TerritorialOffice;

namespace CB.Application.Interfaces.Services
{
    public interface ITerritorialOfficeService
    {
        Task<List<TerritorialOfficeGetDTO>> GetAllAsync();
        Task<TerritorialOfficeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TerritorialOfficeCreateDTO dto);
        Task<bool> UpdateAsync(int id, TerritorialOfficeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
