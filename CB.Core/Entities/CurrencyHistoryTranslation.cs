

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int CurrencyHistoryId { get; set; }
        public CurrencyHistory CurrencyHistory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
