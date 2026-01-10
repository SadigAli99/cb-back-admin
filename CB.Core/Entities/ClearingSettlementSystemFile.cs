
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ClearingSettlementSystemFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int ClearingSettlementSystemId { get; set; }
        public ClearingSettlementSystem ClearingSettlementSystem { get; set; } = null!;
        public List<ClearingSettlementSystemFileTranslation> Translations { get; set; } = new();
    }
}
