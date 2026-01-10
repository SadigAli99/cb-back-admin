
namespace CB.Core.Entities
{
    public class InsuranceStatisticSubCategory : BaseEntity
    {
        public int? InsuranceStatisticCategoryId { get; set; }
        public InsuranceStatisticCategory? InsuranceStatisticCategory { get; set; }
        public List<InsuranceStatisticSubCategoryTranslation> Translations { get; set; } = new();
    }
}
