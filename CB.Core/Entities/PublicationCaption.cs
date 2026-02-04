

namespace CB.Core.Entities
{
    public class PublicationCaption : BaseEntity
    {
        public List<PublicationCaptionTranslation>Translations {get; set; } = new();
    }
}
