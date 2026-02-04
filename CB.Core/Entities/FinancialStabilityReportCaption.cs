

namespace CB.Core.Entities
{
    public class FinancialStabilityReportCaption : BaseEntity
    {
        public List<FinancialStabilityReportCaptionTranslation>Translations {get; set; } = new();
    }
}
