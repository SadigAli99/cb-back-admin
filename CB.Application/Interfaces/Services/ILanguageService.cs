
namespace CB.Application.Interfaces.Services
{
    public interface ILanguageService
    {
        Task<List<string>> GetLanguageCodes();
    }
}
