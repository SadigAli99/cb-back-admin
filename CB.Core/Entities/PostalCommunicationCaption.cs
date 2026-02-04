

namespace CB.Core.Entities
{
    public class PostalCommunicationCaption : BaseEntity
    {
        public List<PostalCommunicationCaptionTranslation>Translations {get; set; } = new();
    }
}
