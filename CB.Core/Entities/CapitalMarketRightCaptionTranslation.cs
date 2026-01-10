
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketRightCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CapitalMarketRightCaptionId { get; set; }
        public CapitalMarketRightCaption CapitalMarketRightCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
