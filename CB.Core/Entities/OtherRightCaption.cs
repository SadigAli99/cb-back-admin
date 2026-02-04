

namespace CB.Core.Entities
{
    public class OtherRightCaption : BaseEntity
    {
        public List<OtherRightCaptionTranslation>Translations {get; set; } = new();
    }
}
