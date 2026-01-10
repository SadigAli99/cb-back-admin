
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OperatorBankCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int OperatorBankCaptionId { get; set; }
        public OperatorBankCaption OperatorBankCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
