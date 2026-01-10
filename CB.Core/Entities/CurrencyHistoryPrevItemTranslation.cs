
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItemTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CurrencyHistoryPrevItemId { get; set; }
        public CurrencyHistoryPrevItem CurrencyHistoryPrevItem { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
