

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CapitalMarketLawId { get; set; }
        public CapitalMarketLaw? CapitalMarketLaw { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
