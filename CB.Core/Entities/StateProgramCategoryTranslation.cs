

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StateProgramCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int StateProgramCategoryId { get; set; }
        public StateProgramCategory StateProgramCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
