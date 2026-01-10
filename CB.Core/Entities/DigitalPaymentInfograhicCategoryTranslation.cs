
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPaymentInfograhicCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int DigitalPaymentInfograhicCategoryId { get; set; }
        public DigitalPaymentInfograhicCategory DigitalPaymentInfograhicCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
