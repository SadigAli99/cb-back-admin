
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class GovernmentPaymentPortalTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; } = string.Empty;
        public int GovernmentPaymentPortalId { get; set; }
        public GovernmentPaymentPortal GovernmentPaymentPortal { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
