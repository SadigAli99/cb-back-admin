
namespace CB.Core.Entities
{
    public class FaqCategory : BaseEntity
    {
        public List<FaqCategoryTranslation> Translations { get; set; } = new();
    }
}
