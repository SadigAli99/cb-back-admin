
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StaffArticleTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(500)]
        public string? SubTitle { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StaffArticleId { get; set; }
        public StaffArticle StaffArticle { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
