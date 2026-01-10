
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FraudStatisticCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FraudStatisticCaptionId { get; set; }
        public FraudStatisticCaption FraudStatisticCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
