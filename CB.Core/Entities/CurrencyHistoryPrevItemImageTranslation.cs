
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItemCharacteristicImageTranslation : BaseEntity
    {
        [StringLength(20)]
        public string? Color { get; set; }
        [StringLength(100)]
        public string? Size { get; set; }
        public int CurrencyHistoryPrevItemCharacteristicImageId { get; set; }
        public CurrencyHistoryPrevItemCharacteristicImage CurrencyHistoryPrevItemCharacteristicImage { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
