
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MarketInformationTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int MarketInformationId { get; set; }
        public MarketInformation? MarketInformation { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
