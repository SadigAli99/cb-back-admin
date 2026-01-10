

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItemCharacteristic : BaseEntity
    {
        public int CurrencyHistoryPrevId { get; set; }
        public CurrencyHistoryPrev CurrencyHistoryPrev { get; set; } = null!;
        public List<CurrencyHistoryPrevItemCharacteristicTranslation> Translations { get; set; } = new();

    }
}
