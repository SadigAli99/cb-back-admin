
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StaffArticleCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StaffArticleCaptionId { get; set; }
        public StaffArticleCaption StaffArticleCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
