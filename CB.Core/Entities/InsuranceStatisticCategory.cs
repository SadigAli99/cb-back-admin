
namespace CB.Core.Entities
{
    public class InsuranceStatisticCategory : BaseEntity
    {
        public List<InsuranceStatisticCategoryTranslation> Translations { get; set; } = new();
    }
}
