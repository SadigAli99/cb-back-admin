

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentStatisticTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentStatisticId { get; set; }
        public PaymentStatistic? PaymentStatistic { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
