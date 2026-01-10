
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NationalBankNoteCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int NationalBankNoteCaptionId { get; set; }
        public NationalBankNoteCaption NationalBankNoteCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
