

namespace CB.Core.Entities
{
    public class ActuaryCaption : BaseEntity
    {
        public List<ActuaryCaptionTranslation>Translations {get; set; } = new();
    }
}
