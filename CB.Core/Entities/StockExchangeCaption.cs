

namespace CB.Core.Entities
{
    public class StockExchangeCaption : BaseEntity
    {
        public List<StockExchangeCaptionTranslation>Translations {get; set; } = new();
    }
}
