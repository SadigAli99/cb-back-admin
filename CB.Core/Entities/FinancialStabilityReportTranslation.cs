

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialStabilityReportTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int FinancialStabilityReportId { get; set; }
        public FinancialStabilityReport? FinancialStabilityReport { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
