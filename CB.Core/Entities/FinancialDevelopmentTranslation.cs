
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialDevelopmentTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        [StringLength(500)]
        public string? FileHeadTitle { get; set; }
        [StringLength(500)]
        public string? FileTitle { get; set; }
        public int FinancialDevelopmentId { get; set; }
        public FinancialDevelopment? FinancialDevelopment { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
