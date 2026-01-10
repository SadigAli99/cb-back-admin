using CB.Application.DTOs.Translate;

namespace CB.Application.Interfaces.Services
{
    public interface ITranslateService
    {
        Task<List<TranslateGetDTO>> GetAllAsync();
        Task<TranslateGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TranslateCreateDTO dto);
        Task<bool> UpdateAsync(int id, TranslateEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<string> GetValueAsync(string key, string lang);
    }
}
