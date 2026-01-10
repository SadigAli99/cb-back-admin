
namespace CB.Core.Entities
{
    public class ReviewApplication : BaseEntity
    {
        public List<ReviewApplicationTranslation> Translations { get; set; } = new();
    }
}
