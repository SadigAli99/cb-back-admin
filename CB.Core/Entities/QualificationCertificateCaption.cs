

namespace CB.Core.Entities
{
    public class QualificationCertificateCaption : BaseEntity
    {
        public List<QualificationCertificateCaptionTranslation>Translations {get; set; } = new();
    }
}
