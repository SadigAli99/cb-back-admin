

namespace CB.Core.Entities
{
    public class CurrencyExchangeCaption : BaseEntity
    {
        public List<CurrencyExchangeCaptionTranslation>? Translations { get; set; }
    }
}
