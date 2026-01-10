using CB.Application.DTOs.MoneySignCharacteristic;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignCharacteristicService
    {
        Task<List<MoneySignCharacteristicGetDTO>> GetAllAsync();
        Task<MoneySignCharacteristicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MoneySignCharacteristicCreateDTO dto);
        Task<bool> UpdateAsync(int id, MoneySignCharacteristicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
