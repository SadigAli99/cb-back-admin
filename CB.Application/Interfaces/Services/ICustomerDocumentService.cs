
using CB.Application.DTOs.CustomerDocument;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerDocumentService
    {
        Task<List<CustomerDocumentGetDTO>> GetAllAsync();
        Task<CustomerDocumentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerDocumentCreateDTO dto);
        Task<bool> UpdateAsync(int id, CustomerDocumentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
