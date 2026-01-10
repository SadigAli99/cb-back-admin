using CB.Application.DTOs.CoinMoneySignCharacteristic;

namespace CB.Application.Interfaces.Services
{
    public interface ICoinMoneySignCharacteristicService
    {
        Task<List<CoinMoneySignCharacteristicGetDTO>> GetAllAsync();
        Task<CoinMoneySignCharacteristicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CoinMoneySignCharacteristicCreateDTO dto);
        Task<bool> UpdateAsync(int id, CoinMoneySignCharacteristicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
