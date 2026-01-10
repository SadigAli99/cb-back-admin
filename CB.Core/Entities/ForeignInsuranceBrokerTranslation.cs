

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ForeignInsuranceBrokerTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int ForeignInsuranceBrokerId { get; set; }
        public ForeignInsuranceBroker? ForeignInsuranceBroker { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
