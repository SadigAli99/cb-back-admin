
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialStabilityCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialStabilityCaptionId { get; set; }
        public FinancialStabilityCaption FinancialStabilityCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
