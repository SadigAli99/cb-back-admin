

namespace CB.Core.Entities
{
    public class InvestmentCompanyCaption : BaseEntity
    {
        public List<InvestmentCompanyCaptionTranslation>Translations {get; set; } = new();
    }
}
