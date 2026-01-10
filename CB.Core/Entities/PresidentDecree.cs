

namespace CB.Core.Entities
{
    public class PresidentDecree : BaseEntity
    {
        public List<PresidentDecreeTranslation> Translations { get; set; } = new();
    }
}
