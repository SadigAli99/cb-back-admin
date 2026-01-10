
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancingActivityCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancingActivityCaptionId { get; set; }
        public FinancingActivityCaption FinancingActivityCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
