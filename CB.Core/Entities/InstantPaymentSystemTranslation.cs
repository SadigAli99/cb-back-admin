
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InstantPaymentSystemTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InstantPaymentSystemId { get; set; }
        public InstantPaymentSystem InstantPaymentSystem { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
