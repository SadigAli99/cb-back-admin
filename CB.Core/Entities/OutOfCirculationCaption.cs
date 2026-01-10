

namespace CB.Core.Entities
{
    public class OutOfCirculationCaption : BaseEntity
    {
        public List<OutOfCirculationCaptionTranslation> Translations { get; set; } = new();
    }
}
