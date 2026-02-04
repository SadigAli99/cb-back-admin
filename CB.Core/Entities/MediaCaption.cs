

namespace CB.Core.Entities
{
    public class MediaCaption : BaseEntity
    {
        public List<MediaCaptionTranslation> Translations { get; set; } = new();
    }
}
