

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FraudStatisticTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int FraudStatisticId { get; set; }
        public FraudStatistic FraudStatistic { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
