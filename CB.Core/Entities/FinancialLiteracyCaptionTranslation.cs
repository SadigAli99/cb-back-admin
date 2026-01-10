
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialLiteracyCaptionId { get; set; }
        public FinancialLiteracyCaption FinancialLiteracyCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
