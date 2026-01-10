

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevItemCharacteristicImage : BaseEntity
    {
        [StringLength(100)]
        public string? Nominal { get; set; }
        [StringLength(100)]
        public string? FrontImage { get; set; }
        [StringLength(100)]
        public string? BackImage { get; set; }
        public int CurrencyHistoryPrevId { get; set; }
        public CurrencyHistoryPrev CurrencyHistoryPrev { get; set; } = null!;
        public List<CurrencyHistoryPrevItemCharacteristicImageTranslation> Translations { get; set; } = new();

    }
}
