
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialDevelopment : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        [StringLength(100)]
        public string? PdfFile { get; set; }
        public List<FinancialDevelopmentTranslation>? Translations { get; set; }
    }
}
