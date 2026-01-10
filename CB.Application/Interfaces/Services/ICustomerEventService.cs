
using CB.Application.DTOs.CustomerEvent;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerEventService
    {
        Task<List<CustomerEventGetDTO>> GetAllAsync();
        Task<CustomerEventGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerEventCreateDTO dto);
        Task<bool> UpdateAsync(int id, CustomerEventEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
