

namespace CB.Core.Entities
{
    public class FraudStatisticCaption : BaseEntity
    {
        public List<FraudStatisticCaptionTranslation> Translations { get; set; } = new();
    }
}
