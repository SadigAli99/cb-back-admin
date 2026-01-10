

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentFundTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InvestmentFundId { get; set; }
        public InvestmentFund? InvestmentFund { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
