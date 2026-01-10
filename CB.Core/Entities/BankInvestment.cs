

namespace CB.Core.Entities
{
    public class BankInvestment : BaseEntity
    {
        public List<BankInvestmentTranslation> Translations { get; set; } = new();
    }
}
