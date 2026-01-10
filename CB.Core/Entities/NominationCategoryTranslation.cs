

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NominationCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int NominationCategoryId { get; set; }
        public NominationCategory NominationCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
