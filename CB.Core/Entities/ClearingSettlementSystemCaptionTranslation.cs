
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ClearingSettlementSystemCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ClearingSettlementSystemCaptionId { get; set; }
        public ClearingSettlementSystemCaption ClearingSettlementSystemCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
