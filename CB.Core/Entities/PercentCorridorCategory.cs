
namespace CB.Core.Entities
{
    public class PercentCorridorCategory : BaseEntity
    {
        public List<PercentCorridorCategoryTranslation> Translations { get; set; } = new();
    }
}
