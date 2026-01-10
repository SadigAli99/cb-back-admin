

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyRegulationLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CurrencyRegulationLawId { get; set; }
        public CurrencyRegulationLaw? CurrencyRegulationLaw { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
