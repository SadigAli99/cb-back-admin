

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentSecurityTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InvestmentSecurityId { get; set; }
        public InvestmentSecurity? InvestmentSecurity { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
