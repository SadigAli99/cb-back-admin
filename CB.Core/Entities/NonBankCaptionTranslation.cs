
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NonBankCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int NonBankCaptionId { get; set; }
        public NonBankCaption NonBankCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
