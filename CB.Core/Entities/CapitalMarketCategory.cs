
namespace CB.Core.Entities
{
    public class CapitalMarketCategory : BaseEntity
    {
        public List<CapitalMarketCategoryTranslation> Translations { get; set; } = new();
    }
}
