
using CB.Application.DTOs.FraudStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface IFraudStatisticService
    {
        Task<List<FraudStatisticGetDTO>> GetAllAsync();
        Task<FraudStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FraudStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, FraudStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
