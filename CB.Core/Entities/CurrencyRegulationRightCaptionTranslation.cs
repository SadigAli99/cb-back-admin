
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyRegulationRightCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CurrencyRegulationRightCaptionId { get; set; }
        public CurrencyRegulationRightCaption CurrencyRegulationRightCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
