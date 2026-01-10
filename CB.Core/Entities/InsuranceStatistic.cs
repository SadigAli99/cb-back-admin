
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceStatistic : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int? InsuranceStatisticCategoryId { get; set; }
        public InsuranceStatisticCategory? InsuranceStatisticCategory { get; set; }
        public int? InsuranceStatisticSubCategoryId { get; set; }
        public InsuranceStatisticSubCategory? InsuranceStatisticSubCategory { get; set; }
        public List<InsuranceStatisticTranslation> Translations { get; set; } = new();
    }
}
