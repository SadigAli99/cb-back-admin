
using CB.Application.DTOs.CustomerContact;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerContactService
    {
        Task<List<CustomerContactGetDTO>> GetAllAsync();
        Task<CustomerContactGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerContactCreateDTO dto);
        Task<bool> UpdateAsync(int id, CustomerContactEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
