

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CapitalMarketRightId { get; set; }
        public CapitalMarketRight? CapitalMarketRight { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
