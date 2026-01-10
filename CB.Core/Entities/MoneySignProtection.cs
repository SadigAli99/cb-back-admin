

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class MoneySignProtection : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;

    }
}
