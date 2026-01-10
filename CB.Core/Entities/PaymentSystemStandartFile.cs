
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemStandartFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int PaymentSystemStandartId { get; set; }
        public PaymentSystemStandart PaymentSystemStandart { get; set; } = null!;
        public List<PaymentSystemStandartFileTranslation> Translations { get; set; } = new();
    }
}
