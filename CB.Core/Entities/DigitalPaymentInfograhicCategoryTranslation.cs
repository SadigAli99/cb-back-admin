
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPaymentInfographicCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int DigitalPaymentInfographicCategoryId { get; set; }
        public DigitalPaymentInfographicCategory DigitalPaymentInfographicCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
