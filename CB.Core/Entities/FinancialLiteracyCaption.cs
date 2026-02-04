

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyCaption : BaseEntity
    {
        [StringLength(5000)]
        public string? Url { get; set; }
        public List<FinancialLiteracyCaptionTranslation>Translations {get; set; } = new();
    }
}
