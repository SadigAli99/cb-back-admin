

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItem : BaseEntity
    {
        public int CurrencyHistoryPrevId { get; set; }
        public CurrencyHistoryPrev CurrencyHistoryPrev { get; set; } = null!;
        public List<CurrencyHistoryPrevItemTranslation> Translations { get; set; } = new();

    }
}
