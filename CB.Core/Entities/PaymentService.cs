
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentService : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<PaymentServiceTranslation> Translations { get; set; } = new();
    }
}
