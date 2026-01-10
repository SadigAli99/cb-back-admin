using CB.Application.DTOs.DigitalPaymentInfograhicItem;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentInfograhicItemService
    {
        Task<List<DigitalPaymentInfograhicItemGetDTO>> GetAllAsync();
        Task<DigitalPaymentInfograhicItemGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentInfograhicItemCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentInfograhicItemEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
