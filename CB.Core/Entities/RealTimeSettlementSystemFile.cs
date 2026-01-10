
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystemFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int RealTimeSettlementSystemId { get; set; }
        public RealTimeSettlementSystem RealTimeSettlementSystem { get; set; } = null!;
        public List<RealTimeSettlementSystemFileTranslation> Translations { get; set; } = new();
    }
}
