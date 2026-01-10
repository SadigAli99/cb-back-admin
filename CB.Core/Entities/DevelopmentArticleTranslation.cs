
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DevelopmentArticleTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int DevelopmentArticleId { get; set; }
        public DevelopmentArticle DevelopmentArticle { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
