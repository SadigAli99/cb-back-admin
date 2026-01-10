
using CB.Application.DTOs.TerritorialOfficeRegion;

namespace CB.Application.Interfaces.Services
{
    public interface ITerritorialOfficeRegionService
    {
        Task<List<TerritorialOfficeRegionGetDTO>> GetAllAsync();
        Task<TerritorialOfficeRegionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TerritorialOfficeRegionCreateDTO dto);
        Task<bool> UpdateAsync(int id, TerritorialOfficeRegionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
