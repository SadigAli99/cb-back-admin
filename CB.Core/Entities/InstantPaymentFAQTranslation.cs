

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InstantPaymentFAQTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InstantPaymentFAQId { get; set; }
        public InstantPaymentFAQ? InstantPaymentFAQ { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
