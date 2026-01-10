

namespace CB.Core.Entities
{
    public class Insurer : BaseEntity
    {
        public List<InsurerTranslation> Translations { get; set; } = new();
    }
}
