
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialSearchSystemTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialSearchSystemId { get; set; }
        public FinancialSearchSystem FinancialSearchSystem { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
