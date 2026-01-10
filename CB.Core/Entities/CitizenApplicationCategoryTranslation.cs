

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CitizenApplicationCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int CitizenApplicationCategoryId { get; set; }
        public CitizenApplicationCategory CitizenApplicationCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
