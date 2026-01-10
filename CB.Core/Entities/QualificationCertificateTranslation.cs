

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class QualificationCertificateTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int QualificationCertificateId { get; set; }
        public QualificationCertificate? QualificationCertificate { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
