

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int CapitalMarketFileId { get; set; }
        public CapitalMarketFile? CapitalMarketFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
