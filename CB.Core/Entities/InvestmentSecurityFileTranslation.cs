

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentSecurityFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InvestmentSecurityFileId { get; set; }
        public InvestmentSecurityFile? InvestmentSecurityFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
