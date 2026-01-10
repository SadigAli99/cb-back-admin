
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignHistoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(10000)]
        public string? Description { get; set; }
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
