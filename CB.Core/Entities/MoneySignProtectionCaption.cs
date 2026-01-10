

namespace CB.Core.Entities
{
    public class MoneySignProtectionCaption : BaseEntity
    {
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory? MoneySignHistory { get; set; }
        public List<MoneySignProtectionCaptionTranslation>? Translations { get; set; }
    }
}
