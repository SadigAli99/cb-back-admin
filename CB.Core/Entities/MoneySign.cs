

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class MoneySign : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public MoneySignType Type { get; set; }
        public List<MoneySignHistory> Histories { get; set; } = new();
        public List<MoneySignTranslation> Translations { get; set; } = new();

    }
}
