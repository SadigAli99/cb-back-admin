

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyEventCaption : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<FinancialLiteracyEventCaptionTranslation>Translations {get; set; } = new();
    }
}
