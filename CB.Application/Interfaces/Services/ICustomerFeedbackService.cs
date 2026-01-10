
using CB.Application.DTOs.CustomerFeedback;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerFeedbackService
    {
        Task<List<CustomerFeedbackGetDTO>> GetAllAsync();
        Task<CustomerFeedbackGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerFeedbackCreateDTO dto);
        Task<bool> UpdateAsync(int id, CustomerFeedbackEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
