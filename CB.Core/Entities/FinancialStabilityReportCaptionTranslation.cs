
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialStabilityReportCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialStabilityReportCaptionId { get; set; }
        public FinancialStabilityReportCaption FinancialStabilityReportCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
