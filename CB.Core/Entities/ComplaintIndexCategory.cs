
namespace CB.Core.Entities
{
    public class ComplaintIndexCategory : BaseEntity
    {
        public List<ComplaintIndexCategoryTranslation> Translations { get; set; } = new();
    }
}
