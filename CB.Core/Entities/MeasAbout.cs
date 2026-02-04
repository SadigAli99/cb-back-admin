

namespace CB.Core.Entities
{
    public class MeasAbout : BaseEntity
    {
        public List<MeasAboutTranslation> Translations { get; set; } = new();
    }
}
