

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyRegulationRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CurrencyRegulationRightId { get; set; }
        public CurrencyRegulationRight? CurrencyRegulationRight { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
