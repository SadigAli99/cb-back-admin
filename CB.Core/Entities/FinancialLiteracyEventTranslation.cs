
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyEventTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialLiteracyEventId { get; set; }
        public FinancialLiteracyEvent FinancialLiteracyEvent { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
