
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentCompanyFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<InvestmentCompanyFileTranslation> Translations { get; set; } = new();
    }
}
