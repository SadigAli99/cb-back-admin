

namespace CB.Core.Entities
{
    public class StockExchange : BaseEntity
    {
        public List<StockExchangeTranslation> Translations { get; set; } = new();
    }
}
