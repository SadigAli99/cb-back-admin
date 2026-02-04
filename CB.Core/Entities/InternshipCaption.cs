

namespace CB.Core.Entities
{
    public class InternshipCaption : BaseEntity
    {
        public List<InternshipCaptionTranslation> Translations { get; set; } = new();
    }
}
