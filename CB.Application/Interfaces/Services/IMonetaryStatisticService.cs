
using CB.Application.DTOs.MonetaryStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryStatisticService
    {
        Task<List<MonetaryStatisticGetDTO>> GetAllAsync();
        Task<MonetaryStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MonetaryStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
