

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialSearchSystemCaption : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<FinancialSearchSystemCaptionTranslation>? Translations { get; set; }
    }
}
