

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ControlMeasureCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int ControlMeasureCategoryId { get; set; }
        public ControlMeasureCategory ControlMeasureCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
