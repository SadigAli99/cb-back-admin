

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ClearingSettlementSystemFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ClearingSettlementSystemFileId { get; set; }
        public ClearingSettlementSystemFile ClearingSettlementSystemFile { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
