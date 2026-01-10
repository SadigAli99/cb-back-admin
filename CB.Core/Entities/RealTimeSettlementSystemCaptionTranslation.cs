
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystemCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int RealTimeSettlementSystemCaptionId { get; set; }
        public RealTimeSettlementSystemCaption RealTimeSettlementSystemCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
