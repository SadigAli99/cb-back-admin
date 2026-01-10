
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class QualificationCertificateCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int QualificationCertificateCaptionId { get; set; }
        public QualificationCertificateCaption QualificationCertificateCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
