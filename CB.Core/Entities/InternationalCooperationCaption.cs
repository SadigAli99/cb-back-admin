

namespace CB.Core.Entities
{
    public class InternationalCooperationCaption : BaseEntity
    {
        public List<InternationalCooperationCaptionTranslation>Translations {get; set; } = new();
    }
}
