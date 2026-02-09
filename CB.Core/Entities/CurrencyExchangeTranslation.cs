

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyExchangeTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int CurrencyExchangeId { get; set; }
        public CurrencyExchange? CurrencyExchange { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
