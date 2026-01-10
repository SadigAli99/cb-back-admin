
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialFlow : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        public int Year { get; set; }
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<FinancialFlowTranslation> Translations { get; set; } = new();
    }
}
