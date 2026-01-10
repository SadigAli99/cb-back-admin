
using CB.Application.DTOs.CoinMoneySignCharacteristicImage;

namespace CB.Application.Interfaces.Services
{
    public interface ICoinMoneySignCharacteristicImageService
    {
        Task<List<CoinMoneySignCharacteristicImageGetDTO>> GetAllAsync();
        Task<CoinMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CoinMoneySignCharacteristicImageCreateDTO dto);
        Task<bool> UpdateAsync(int id, CoinMoneySignCharacteristicImageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
