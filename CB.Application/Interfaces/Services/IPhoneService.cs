using CB.Application.DTOs.Phone;

namespace CB.Application.Interfaces.Services
{
    public interface IPhoneService
    {
        Task<List<PhoneGetDTO>> GetAllAsync();
        Task<PhoneGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PhoneCreateDTO dto);
        Task<bool> UpdateAsync(int id, PhoneEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
