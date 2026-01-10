
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystemTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int RealTimeSettlementSystemId { get; set; }
        public RealTimeSettlementSystem RealTimeSettlementSystem { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
