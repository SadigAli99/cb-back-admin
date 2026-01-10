
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankInvestmentFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<BankInvestmentFileTranslation> Translations { get; set; } = new();
    }
}
