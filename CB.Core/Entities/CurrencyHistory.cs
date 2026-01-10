
namespace CB.Core.Entities
{
    public class CurrencyHistory : BaseEntity
    {
        public List<CurrencyHistoryTranslation> Translations { get; set; } = new();
        public List<CurrencyHistoryPrev> Prevs { get; set; } = new();
    }
}
