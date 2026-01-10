
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankInvestmentCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int BankInvestmentCaptionId { get; set; }
        public BankInvestmentCaption BankInvestmentCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
