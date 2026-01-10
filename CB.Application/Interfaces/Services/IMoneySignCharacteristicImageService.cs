
using CB.Application.DTOs.MoneySignCharacteristicImage;

namespace CB.Application.Interfaces.Services
{
    public interface IMoneySignCharacteristicImageService
    {
        Task<List<MoneySignCharacteristicImageGetDTO>> GetAllAsync();
        Task<MoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MoneySignCharacteristicImageCreateDTO dto);
        Task<bool> UpdateAsync(int id, MoneySignCharacteristicImageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
