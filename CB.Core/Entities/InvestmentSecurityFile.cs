
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentSecurityFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int InvestmentSecurityId { get; set; }
        public InvestmentSecurity InvestmentSecurity { get; set; } = null!;
        public List<InvestmentSecurityFileTranslation> Translations { get; set; } = new();
    }
}
