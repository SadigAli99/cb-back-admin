
namespace CB.Core.Entities
{
    public class StateProgramCategory : BaseEntity
    {
        public List<StateProgramCategoryTranslation> Translations { get; set; } = new();
    }
}
