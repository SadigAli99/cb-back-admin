

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankInvestmentFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int BankInvestmentFileId { get; set; }
        public BankInvestmentFile? BankInvestmentFile { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
