using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristic;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfBankNoteMoneySignCharacteristicService
    {
        Task<List<OutOfBankNoteMoneySignCharacteristicGetDTO>> GetAllAsync();
        Task<OutOfBankNoteMoneySignCharacteristicGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(OutOfBankNoteMoneySignCharacteristicCreateDTO dto);
        Task<bool> UpdateAsync(int id, OutOfBankNoteMoneySignCharacteristicEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
