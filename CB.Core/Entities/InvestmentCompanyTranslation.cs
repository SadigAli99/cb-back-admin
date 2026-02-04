

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentCompanyTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InvestmentCompanyId { get; set; }
        public InvestmentCompany? InvestmentCompany { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
