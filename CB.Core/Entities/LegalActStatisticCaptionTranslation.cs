
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LegalActStatisticCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int LegalActStatisticCaptionId { get; set; }
        public LegalActStatisticCaption LegalActStatisticCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
