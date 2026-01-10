
namespace CB.Core.Entities
{
    public class NominationCategory : BaseEntity
    {
        public List<NominationCategoryTranslation> Translations { get; set; } = new();
    }
}
