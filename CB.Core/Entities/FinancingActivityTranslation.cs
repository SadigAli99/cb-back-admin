

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancingActivityTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int FinancingActivityId { get; set; }
        public FinancingActivity? FinancingActivity { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
