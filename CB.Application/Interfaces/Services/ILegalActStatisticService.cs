
using CB.Application.DTOs.LegalActStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface ILegalActStatisticService
    {
        Task<List<LegalActStatisticGetDTO>> GetAllAsync();
        Task<LegalActStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LegalActStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, LegalActStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
