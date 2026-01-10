

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialFlowTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int FinancialFlowId { get; set; }
        public FinancialFlow? FinancialFlow { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
