
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentInstitutionFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<PaymentInstitutionFileTranslation> Translations { get; set; } = new();
    }
}
