
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ClearingSettlementSystemTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ClearingSettlementSystemId { get; set; }
        public ClearingSettlementSystem ClearingSettlementSystem { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
