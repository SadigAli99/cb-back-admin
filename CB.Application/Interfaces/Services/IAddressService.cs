using CB.Application.DTOs.Address;

namespace CB.Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<List<AddressGetDTO>> GetAllAsync();
        Task<AddressGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AddressCreateDTO dto);
        Task<bool> UpdateAsync(int id, AddressEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
