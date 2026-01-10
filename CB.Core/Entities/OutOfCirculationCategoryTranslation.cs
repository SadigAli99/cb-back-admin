
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OutOfCirculationCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        public int OutOfCirculationCategoryId { get; set; }
        public OutOfCirculationCategory OutOfCirculationCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
