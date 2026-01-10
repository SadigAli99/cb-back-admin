using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryPrevItemCharacteristicService
    {
        Task<List<CurrencyHistoryPrevItemCharacteristicGetDTO>> GetAllAsync();
        Task<CurrencyHistoryPrevItemCharacteristicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryPrevItemCharacteristicCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemCharacteristicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
