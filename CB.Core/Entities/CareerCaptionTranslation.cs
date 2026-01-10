
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CareerCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CareerCaptionId { get; set; }
        public CareerCaption CareerCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
