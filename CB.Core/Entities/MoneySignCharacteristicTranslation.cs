
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignCharacteristicTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Label { get; set; }
        [StringLength(500)]
        public string? Value { get; set; }
        public int MoneySignCharacteristicId { get; set; }
        public MoneySignCharacteristic MoneySignCharacteristic { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
