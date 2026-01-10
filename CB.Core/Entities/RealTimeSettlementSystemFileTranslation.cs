

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystemFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int RealTimeSettlementSystemFileId { get; set; }
        public RealTimeSettlementSystemFile RealTimeSettlementSystemFile { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
