

namespace CB.Core.Entities
{
    public class CreditInstitutionRightCaption : BaseEntity
    {
        public List<CreditInstitutionRightCaptionTranslation> Translations { get; set; } = new();
    }
}
