using CB.Application.DTOs.OutOfCoinMoneySignCharacteristic;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCoinMoneySignCharacteristicService
    {
        Task<List<OutOfCoinMoneySignCharacteristicGetDTO>> GetAllAsync();
        Task<OutOfCoinMoneySignCharacteristicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfCoinMoneySignCharacteristicCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfCoinMoneySignCharacteristicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
