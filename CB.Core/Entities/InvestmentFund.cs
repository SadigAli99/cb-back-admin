

namespace CB.Core.Entities
{
    public class InvestmentFund : BaseEntity
    {
        public List<InvestmentFundTranslation> Translations { get; set; } = new();
    }
}
