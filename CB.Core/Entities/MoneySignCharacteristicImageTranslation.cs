
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignCharacteristicImageTranslation : BaseEntity
    {
        [StringLength(20)]
        public string? Color { get; set; }
        [StringLength(100)]
        public string? Size { get; set; }
        public int MoneySignCharacteristicImageId { get; set; }
        public MoneySignCharacteristicImage MoneySignCharacteristicImage { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
