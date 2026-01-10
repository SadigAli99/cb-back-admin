

namespace CB.Core.Entities
{
    public class DevelopmentArticle : BaseEntity
    {
        public List<DevelopmentArticleTranslation> Translations { get; set; } = new();
    }
}
