
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItemCharacteristicTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Label { get; set; }
        [StringLength(500)]
        public string? Value { get; set; }
        public int CurrencyHistoryPrevItemCharacteristicId { get; set; }
        public CurrencyHistoryPrevItemCharacteristic CurrencyHistoryPrevItemCharacteristic { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
