
using CB.Application.DTOs.DevelopmentArticle;

namespace CB.Application.Interfaces.Services
{
    public interface IDevelopmentArticleService
    {
        Task<bool> CreateOrUpdate(DevelopmentArticlePostDTO dTO);
        Task<DevelopmentArticleGetDTO> GetFirst();
    }
}
