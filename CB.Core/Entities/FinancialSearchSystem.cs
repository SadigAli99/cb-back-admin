
namespace CB.Core.Entities
{
    public class FinancialSearchSystem : BaseEntity
    {
        public List<FinancialSearchSystemTranslation> Translations { get; set; } = new();
        public List<FinancialSearchSystemImage> Images { get; set; } = new();
    }
}
