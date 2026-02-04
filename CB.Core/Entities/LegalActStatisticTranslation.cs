

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LegalActStatisticTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int LegalActStatisticId { get; set; }
        public LegalActStatistic? LegalActStatistic { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
