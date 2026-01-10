
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneySignProtectionCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MoneySignProtectionCaptionId { get; set; }
        public MoneySignProtectionCaption MoneySignProtectionCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
