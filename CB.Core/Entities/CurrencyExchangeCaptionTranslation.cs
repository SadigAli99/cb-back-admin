
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyExchangeCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CurrencyExchangeCaptionId { get; set; }
        public CurrencyExchangeCaption CurrencyExchangeCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
