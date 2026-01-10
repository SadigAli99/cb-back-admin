
using CB.Application.DTOs.TechnicalDocument;

namespace CB.Application.Interfaces.Services
{
    public interface ITechnicalDocumentService
    {
        Task<List<TechnicalDocumentGetDTO>> GetAllAsync();
        Task<TechnicalDocumentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TechnicalDocumentCreateDTO dto);
        Task<bool> UpdateAsync(int id, TechnicalDocumentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
