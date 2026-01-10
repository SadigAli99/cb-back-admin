
namespace CB.Core.Entities
{
    public class MoneySignHistoryFeature : BaseEntity
    {
        public int MoneySignHistoryId { get; set; }
        public MoneySignHistory MoneySignHistory { get; set; } = null!;
        public List<MoneySignHistoryFeatureTranslation> Translations { get; set; } = new();
    }
}
