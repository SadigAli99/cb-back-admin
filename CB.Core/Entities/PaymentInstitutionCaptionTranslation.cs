
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentInstitutionCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PaymentInstitutionCaptionId { get; set; }
        public PaymentInstitutionCaption PaymentInstitutionCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
