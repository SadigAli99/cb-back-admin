

namespace CB.Core.Entities
{
    public class MissionCaption : BaseEntity
    {
        public List<MissionCaptionTranslation> Translations { get; set; } = new();
    }
}
