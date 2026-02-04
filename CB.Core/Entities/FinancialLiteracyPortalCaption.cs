

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyPortalCaption : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<FinancialLiteracyPortalCaptionTranslation>Translations {get; set; } = new();
    }
}
