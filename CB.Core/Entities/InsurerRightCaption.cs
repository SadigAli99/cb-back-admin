

namespace CB.Core.Entities
{
    public class InsurerRightCaption : BaseEntity
    {
        public List<InsurerRightCaptionTranslation> Translations { get; set; } = new();
    }
}
