
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPortal : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public List<DigitalPortalTranslation> Translations { get; set; } = new();
    }
}
