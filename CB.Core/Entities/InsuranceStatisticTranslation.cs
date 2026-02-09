

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceStatisticTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InsuranceStatisticId { get; set; }
        public InsuranceStatistic? InsuranceStatistic { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
