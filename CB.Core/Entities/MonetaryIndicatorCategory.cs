
namespace CB.Core.Entities
{
    public class MonetaryIndicatorCategory : BaseEntity
    {
        public List<MonetaryIndicatorCategoryTranslation> Translations { get; set; } = new();
    }
}
