
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OutOfCirculationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int OutOfCirculationCaptionId { get; set; }
        public OutOfCirculationCaption OutOfCirculationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
