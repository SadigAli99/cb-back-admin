

namespace CB.Core.Entities
{
    public class FinancialStabilityCaption : BaseEntity
    {
        public List<FinancialStabilityCaptionTranslation>Translations {get; set; } = new();
    }
}
