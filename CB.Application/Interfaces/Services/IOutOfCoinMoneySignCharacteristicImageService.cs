
using CB.Application.DTOs.OutOfCoinMoneySignCharacteristicImage;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfCoinMoneySignCharacteristicImageService
    {
        Task<List<OutOfCoinMoneySignCharacteristicImageGetDTO>> GetAllAsync();
        Task<OutOfCoinMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfCoinMoneySignCharacteristicImageCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfCoinMoneySignCharacteristicImageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
