
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class IndexPeriodCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int IndexPeriodCategoryId { get; set; }
        public IndexPeriodCategory? IndexPeriodCategory { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
