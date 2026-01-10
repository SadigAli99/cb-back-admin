

namespace CB.Core.Entities
{
    public class About : BaseEntity
    {
        public List<AboutTranslation> Translations { get; set; } = new();
    }
}
