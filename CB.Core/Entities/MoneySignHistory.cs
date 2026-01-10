

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignHistory : BaseEntity
    {
        [StringLength(500)]
        public string? Video { get; set; }
        public int MoneySignId { get; set; }
        public MoneySign MoneySign { get; set; } = null!;
        public List<MoneySignHistoryTranslation> Translations { get; set; } = new();

    }
}
