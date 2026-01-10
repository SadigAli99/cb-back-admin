

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class MoneySignProtectionElement : BaseEntity
    {
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;
        public List<MoneySignProtectionElementTranslation> Translations { get; set; } = new();
        public List<MoneySignProtectionElementImage> Images { get; set; } = new();

    }
}
