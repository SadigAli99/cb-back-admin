

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryIndicatorCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;
        [StringLength(255)]
        public string? Slug { get; set; } = null!;
        [StringLength(1000)]
        public string? Note { get; set; } = null!;

        public int MonetaryIndicatorCategoryId { get; set; }
        public MonetaryIndicatorCategory MonetaryIndicatorCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
