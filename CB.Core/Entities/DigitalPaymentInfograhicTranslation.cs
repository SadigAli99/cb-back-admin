
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPaymentInfograhicTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int DigitalPaymentInfograhicId { get; set; }
        public DigitalPaymentInfograhic DigitalPaymentInfograhic { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
