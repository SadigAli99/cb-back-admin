
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CurrencyCaptionId { get; set; }
        public CurrencyCaption CurrencyCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
