

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class MoneySignCharacteristic : BaseEntity
    {
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;
        public List<MoneySignCharacteristicTranslation> Translations { get; set; } = new();

    }
}
