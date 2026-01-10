
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MonetaryPolicyCaptionId { get; set; }
        public MonetaryPolicyCaption MonetaryPolicyCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
