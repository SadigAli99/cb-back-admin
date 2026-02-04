
namespace CB.Core.Entities
{
    public class CitizenApplication : BaseEntity
    {
        public int CreditInstitutionCount { get; set; } = 0;
        public int PaymentSystemCount { get; set; } = 0;
        public int CurrencyExchangeCount { get; set; } = 0;
        public int InsurerCount { get; set; } = 0;
        public int CapitalMarketCount { get; set; } = 0;
        public int OtherCount { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public int Month { get; set; }
        public int Year { get; set; }
        public int CitizenApplicationCategoryId { get; set; }
        public CitizenApplicationCategory CitizenApplicationCategory { get; set; } = null!;
    }
}
