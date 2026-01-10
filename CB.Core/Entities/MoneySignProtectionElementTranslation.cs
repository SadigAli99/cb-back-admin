
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignProtectionElementTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Value { get; set; }
        public int MoneySignProtectionElementId { get; set; }
        public MoneySignProtectionElement MoneySignProtectionElement { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
