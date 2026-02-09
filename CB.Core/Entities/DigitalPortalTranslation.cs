

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPortalTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Text { get; set; }
        public int DigitalPortalId { get; set; }
        public DigitalPortal? DigitalPortal { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
