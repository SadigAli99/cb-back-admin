
using CB.Application.DTOs.TerritorialOfficeStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface ITerritorialOfficeStatisticService
    {
        Task<List<TerritorialOfficeStatisticGetDTO>> GetAllAsync();
        Task<TerritorialOfficeStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TerritorialOfficeStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, TerritorialOfficeStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
