

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BankSectorCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;
        [StringLength(255)]
        public string? Slug { get; set; } = null!;
        [StringLength(1000)]
        public string? Note { get; set; } = null!;

        public int BankSectorCategoryId { get; set; }
        public BankSectorCategory BankSectorCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
