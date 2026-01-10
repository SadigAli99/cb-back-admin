
using CB.Application.DTOs.CustomerDocumentFile;

namespace CB.Application.Interfaces.Services
{
    public interface ICustomerDocumentFileService
    {
        Task<List<CustomerDocumentFileGetDTO>> GetAllAsync();
        Task<CustomerDocumentFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerDocumentFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, CustomerDocumentFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
