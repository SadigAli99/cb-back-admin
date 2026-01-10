
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditUnionCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CreditUnionCaptionId { get; set; }
        public CreditUnionCaption CreditUnionCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
