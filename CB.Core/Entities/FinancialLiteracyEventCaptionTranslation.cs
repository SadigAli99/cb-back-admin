
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyEventCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int FinancialLiteracyEventCaptionId { get; set; }
        public FinancialLiteracyEventCaption FinancialLiteracyEventCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
