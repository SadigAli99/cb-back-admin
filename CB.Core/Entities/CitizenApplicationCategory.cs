
namespace CB.Core.Entities
{
    public class CitizenApplicationCategory : BaseEntity
    {
        public List<CitizenApplicationCategoryTranslation> Translations { get; set; } = new();
    }
}
