

namespace CB.Core.Entities
{
    public class ClearingSettlementSystem : BaseEntity
    {
        public List<ClearingSettlementSystemTranslation> Translations { get; set; } = new();
    }
}
