
using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfBankNoteMoneySignCharacteristicImageService
    {
        Task<List<OutOfBankNoteMoneySignCharacteristicImageGetDTO>> GetAllAsync();
        Task<OutOfBankNoteMoneySignCharacteristicImageGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfBankNoteMoneySignCharacteristicImageCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignCharacteristicImageEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
