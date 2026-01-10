
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryNext : BaseEntity
    {
        public int CurrencyHistoryId { get; set; }
        public CurrencyHistory CurrencyHistory { get; set; } = null!;
        public List<CurrencyHistoryNextTranslation> Translations { get; set; } = new();
        public List<CurrencyHistoryNextItem> Items { get; set; } = new();
    }
}
