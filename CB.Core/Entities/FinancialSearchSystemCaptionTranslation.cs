
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialSearchSystemCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int FinancialSearchSystemCaptionId { get; set; }
        public FinancialSearchSystemCaption FinancialSearchSystemCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
