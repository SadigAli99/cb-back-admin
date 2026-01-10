
namespace CB.Core.Entities
{
    public class StaffArticle : BaseEntity
    {
        public int Year { get; set; }
        public List<StaffArticleTranslation> Translations { get; set; } = new();
    }
}
