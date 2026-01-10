
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StatisticCaptionId { get; set; }
        public StatisticCaption StatisticCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
