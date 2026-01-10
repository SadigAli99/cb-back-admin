using CB.Application.DTOs.MonetaryIndicator;

namespace CB.Application.Interfaces.Services
{
    public interface IMonetaryIndicatorService
    {
        Task<List<MonetaryIndicatorGetDTO>> GetAllAsync();
        Task<MonetaryIndicatorGetDTO?> GetByIdAsync(int id);
        Task<MonetaryIndicatorEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(MonetaryIndicatorCreateDTO dto);
        Task<bool> UpdateAsync(int id, MonetaryIndicatorEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
