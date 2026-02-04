

namespace CB.Core.Entities
{
    public class NominationCaption : BaseEntity
    {
        public List<NominationCaptionTranslation>Translations {get; set; } = new();
    }
}
