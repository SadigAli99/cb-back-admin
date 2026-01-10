
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        public int MoneySignId { get; set; }
        public MoneySign MoneySign { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
