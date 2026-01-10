
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyPortalCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int FinancialLiteracyPortalCaptionId { get; set; }
        public FinancialLiteracyPortalCaption FinancialLiteracyPortalCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
