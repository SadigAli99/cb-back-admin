

namespace CB.Core.Entities
{
    public class ForeignInsuranceBrokerCaption : BaseEntity
    {
        public List<ForeignInsuranceBrokerCaptionTranslation>Translations {get; set; } = new();
    }
}
