

namespace CB.Core.Entities
{
    public class RealTimeSettlementSystem : BaseEntity
    {
        public List<RealTimeSettlementSystemTranslation> Translations { get; set; } = new();
    }
}
