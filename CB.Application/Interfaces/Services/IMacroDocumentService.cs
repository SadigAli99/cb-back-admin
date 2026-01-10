
using CB.Application.DTOs.MacroDocument;

namespace CB.Application.Interfaces.Services
{
    public interface IMacroDocumentService
    {
        Task<List<MacroDocumentGetDTO>> GetAllAsync();
        Task<MacroDocumentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MacroDocumentCreateDTO dto);
        Task<bool> UpdateAsync(int id, MacroDocumentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
