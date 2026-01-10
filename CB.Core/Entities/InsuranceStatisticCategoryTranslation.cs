
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceStatisticCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int InsuranceStatisticCategoryId { get; set; }
        public InsuranceStatisticCategory InsuranceStatisticCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
