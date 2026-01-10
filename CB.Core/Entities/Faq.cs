
namespace CB.Core.Entities
{
    public class Faq : BaseEntity
    {
        public int? FaqCategoryId { get; set; }
        public FaqCategory? FaqCategory { get; set; }
        public List<FaqTranslation> Translations { get; set; } = new();
    }
}
