

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentFundFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InvestmentFundFileId { get; set; }
        public InvestmentFundFile? InvestmentFundFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
