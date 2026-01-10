
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StockExchangeCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StockExchangeCaptionId { get; set; }
        public StockExchangeCaption StockExchangeCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
