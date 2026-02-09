

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int CapitalMarketId { get; set; }
        public CapitalMarket? CapitalMarket { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
