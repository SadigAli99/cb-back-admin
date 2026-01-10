

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class MoneySignCharacteristicImage : BaseEntity
    {
        [StringLength(100)]
        public string? Nominal { get; set; }
        [StringLength(100)]
        public string? FrontImage { get; set; }
        [StringLength(100)]
        public string? BackImage { get; set; }
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;
        public List<MoneySignCharacteristicImageTranslation> Translations { get; set; } = new();

    }
}
