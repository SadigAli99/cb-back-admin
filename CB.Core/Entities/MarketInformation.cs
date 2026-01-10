
namespace CB.Core.Entities
{
    public class MarketInformation : BaseEntity
    {
        public string? Description { get; set; }
        public List<MarketInformationTranslation> Translations { get; set; } = new();
    }
}
