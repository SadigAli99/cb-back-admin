
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentFundCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InvestmentFundCaptionId { get; set; }
        public InvestmentFundCaption InvestmentFundCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
