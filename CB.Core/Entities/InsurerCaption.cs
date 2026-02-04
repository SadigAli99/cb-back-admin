

namespace CB.Core.Entities
{
    public class InsurerCaption : BaseEntity
    {
        public List<InsurerCaptionTranslation>Translations {get; set; } = new();
    }
}
