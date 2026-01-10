

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankNoteCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;
        [StringLength(100)]
        public string? ShortTitle { get; set; } = null!;
        [StringLength(255)]
        public string? Slug { get; set; } = null!;
        [StringLength(1000)]
        public string? Note { get; set; } = null!;

        public int BankNoteCategoryId { get; set; }
        public BankNoteCategory BankNoteCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
