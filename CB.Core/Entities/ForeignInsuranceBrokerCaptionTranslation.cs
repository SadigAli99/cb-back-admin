
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ForeignInsuranceBrokerCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ForeignInsuranceBrokerCaptionId { get; set; }
        public ForeignInsuranceBrokerCaption ForeignInsuranceBrokerCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
