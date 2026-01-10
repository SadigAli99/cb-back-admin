

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystemCaption : BaseEntity
    {
        public List<RealTimeSettlementSystemCaptionTranslation> Translations { get; set; } = new();
    }
}
