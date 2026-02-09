

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketPresidentActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CapitalMarketPresidentActId { get; set; }
        public CapitalMarketPresidentAct? CapitalMarketPresidentAct { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
