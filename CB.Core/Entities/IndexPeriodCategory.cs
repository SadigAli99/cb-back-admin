
namespace CB.Core.Entities
{
    public class IndexPeriodCategory : BaseEntity
    {
        public List<IndexPeriodCategoryTranslation> Translations { get; set; } = new();
    }
}
