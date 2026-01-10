
using CB.Application.DTOs.InsuranceStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface IInsuranceStatisticService
    {
        Task<List<InsuranceStatisticGetDTO>> GetAllAsync();
        Task<InsuranceStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsuranceStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsuranceStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
