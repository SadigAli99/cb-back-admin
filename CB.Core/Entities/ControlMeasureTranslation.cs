
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ControlMeasureTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(10000)]
        public string? Description { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
        public int ControlMeasureId { get; set; }
        public ControlMeasure ControlMeasure { get; set; } = null!;
    }
}
