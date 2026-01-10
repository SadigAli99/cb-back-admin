
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrev : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int CurrencyHistoryId { get; set; }
        public CurrencyHistory CurrencyHistory { get; set; } = null!;
        public List<CurrencyHistoryPrevTranslation> Translations { get; set; } = new();
        public List<CurrencyHistoryPrevItem> Items { get; set; } = new();
    }
}
