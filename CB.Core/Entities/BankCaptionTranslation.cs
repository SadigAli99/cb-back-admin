
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int BankCaptionId { get; set; }
        public BankCaption BankCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
