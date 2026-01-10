

namespace CB.Core.Entities
{
    public class ClearingSettlementSystemCaption : BaseEntity
    {
        public List<ClearingSettlementSystemCaptionTranslation> Translations { get; set; } = new();
    }
}
