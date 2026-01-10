

namespace CB.Core.Entities
{
    public class InvestmentCompany : BaseEntity
    {
        public List<InvestmentCompanyTranslation> Translations { get; set; } = new();
    }
}
