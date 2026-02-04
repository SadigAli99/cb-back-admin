

namespace CB.Core.Entities
{
    public class StatisticCaption : BaseEntity
    {
        public List<StatisticCaptionTranslation>Translations {get; set; } = new();
    }
}
