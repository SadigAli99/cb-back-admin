

namespace CB.Core.Entities
{
    public class LossAdjusterCaption : BaseEntity
    {
        public List<LossAdjusterCaptionTranslation>Translations {get; set; } = new();
    }
}
