
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceStatisticSubCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int InsuranceStatisticSubCategoryId { get; set; }
        public InsuranceStatisticSubCategory InsuranceStatisticSubCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
