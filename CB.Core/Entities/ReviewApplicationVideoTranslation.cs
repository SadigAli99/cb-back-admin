
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationVideoTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ReviewApplicationVideoId { get; set; }
        public ReviewApplicationVideo ReviewApplicationVideo { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
