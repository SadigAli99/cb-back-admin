
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentAgentFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<PaymentAgentFileTranslation> Translations { get; set; } = new();
    }
}
