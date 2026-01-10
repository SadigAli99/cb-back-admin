
using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage;

namespace CB.Application.Interfaces.Services
{
    public interface ICurrencyHistoryPrevItemCharacteristicImageService
    {
        Task<List<CurrencyHistoryPrevItemCharacteristicImageGetDTO>> GetAllAsync();
        Task<CurrencyHistoryPrevItemCharacteristicImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CurrencyHistoryPrevItemCharacteristicImageCreateDTO dto);
        Task<bool> UpdateAsync(int id, CurrencyHistoryPrevItemCharacteristicImageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
